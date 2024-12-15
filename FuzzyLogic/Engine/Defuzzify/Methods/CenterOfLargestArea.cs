using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Rule;
using static System.Math;
using static FuzzyLogic.Engine.Defuzzify.ImplicationMethod;

// ReSharper disable RedundantExplicitTupleComponentName

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class CenterOfLargestArea : IDefuzzifier
{
    public double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts,
        INegation negation, INorm tNorm, IConorm tConorm, ImplicationMethod method = Mamdani)
    {
        IDefuzzifier.RulesCheck(rules, facts);
        var minValue = rules.Select(e => e.Consequent!.Function).Min(func => func.FiniteSupportLeft());
        var weightedFunctions = rules
            .Select(rule => (
                Function: rule.Consequent!.Function,
                Weight: rule.EvaluatePremiseWeight(facts, negation, tNorm, tConorm)))
            .Where(tuple => tuple.Weight > 0)
            .ToList();
        if (weightedFunctions.Count == 0)
            return minValue;
        var maxArea = weightedFunctions
            .Select(tuple => (
                Area: tuple.Function.CalculateArea(tuple.Weight, method),
                Centroid: tuple.Function.FiniteSupportLeft() + tuple.Function.CentroidXCoordinate(tuple.Weight, method).GetValueOrDefault()))
            .MaxBy(tuple => tuple.Area);
        var maxValue = weightedFunctions.Max(tuple => tuple.Function.FiniteSupportRight());
        return Min(maxArea.Centroid, maxValue);
    }
}
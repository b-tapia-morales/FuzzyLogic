using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Rule;
using static System.Math;
using static FuzzyLogic.Engine.Defuzzify.ImplicationMethod;

// ReSharper disable RedundantExplicitTupleComponentName

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class CenterOfSums : IDefuzzifier
{
    public double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts, INegation negation,
        INorm tNorm, IConorm tConorm, ImplicationMethod method = Mamdani)
    {
        IDefuzzifier.RulesCheck(rules, facts);
        var minValue = rules.Select(e => e.Consequent!.Function).Min(func => func.FiniteSupportLeft());
        var weightedTuples = rules
            .Select(rule => (
                Function: rule.Consequent!.Function,
                Weight: rule.EvaluatePremiseWeight(facts, negation, tNorm, tConorm)))
            .Where(t => t.Weight > 0)
            .ToList();
        if (weightedTuples.Count == 0)
            return minValue;
        var areaCentroidTuples = weightedTuples
            .Select(tuple => (
                Area: tuple.Function.CalculateArea(tuple.Weight, method),
                Centroid: tuple.Function.FiniteSupportLeft() + tuple.Function.CentroidXCoordinate(tuple.Weight, method).GetValueOrDefault()))
            .ToList();
        var maxValue = weightedTuples.Max(tuple => tuple.Function.FiniteSupportRight());
        return Min(areaCentroidTuples.Sum(e => e.Area * e.Centroid) / areaCentroidTuples.Sum(e => e.Area), maxValue);
    }
}
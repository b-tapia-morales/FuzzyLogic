using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Rule;
using static FuzzyLogic.Engine.Defuzzify.ImplicationMethod;

// ReSharper disable RedundantExplicitTupleComponentName

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class CenterOfSums : IDefuzzifier
{
    public double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts, INegation negation,
        INorm tNorm, IConorm tConorm, ImplicationMethod method = Mamdani)
    {
        IDefuzzifier.RulesCheck(rules, facts);
        var tuples = rules
            .Select(rule => (
                Function: rule.Consequent!.Function,
                Weight: rule.EvaluatePremiseWeight(facts, negation, tNorm, tConorm)))
            .Where(t => t.Weight > 0)
            .Select(tuple => (
                Area: tuple.Function.CalculateArea(tuple.Weight, method),
                Centroid: tuple.Function.FiniteSupportLeft() + tuple.Function.CentroidXCoordinate(tuple.Weight, method).GetValueOrDefault()))
            .ToList();
        return tuples.Sum(e => e.Area * e.Centroid) / tuples.Sum(e => e.Area);
    }
}
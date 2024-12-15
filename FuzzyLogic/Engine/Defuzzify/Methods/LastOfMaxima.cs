using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Rule;
using static FuzzyLogic.Engine.Defuzzify.ImplicationMethod;

// ReSharper disable RedundantExplicitTupleComponentName

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class LastOfMaxima : IDefuzzifier
{
    public double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts, INegation negation,
        INorm tNorm, IConorm tConorm, ImplicationMethod method = Mamdani)
    {
        IDefuzzifier.RulesCheck(rules, facts);
        var minValue = rules.Select(e => e.Consequent!.Function).Min(func => func.FiniteSupportLeft());
        var tuple = rules
            .Select(rule => (Function: rule.Consequent!.Function, Weight: rule.EvaluatePremiseWeight(facts, negation, tNorm, tConorm)))
            .MaxBy(tuple => tuple.Weight);
        if (tuple.Weight == 0)
            return minValue;
        var (function, weight) = tuple;
        return method == Mamdani ? function.AlphaCutRight(weight) : function.PeakRight();
    }
}
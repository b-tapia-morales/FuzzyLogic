using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Rule;
using static FuzzyLogic.Engine.Defuzzify.ImplicationMethod;

// ReSharper disable RedundantExplicitTupleComponentName

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class FirstOfMaxima : IDefuzzifier
{
    public double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts, INegation negation,
        INorm tNorm, IConorm tConorm, ImplicationMethod method = Mamdani)
    {
        IDefuzzifier.RulesCheck(rules, facts);
        var tuple = rules
            .Select(e => (Function: e.Consequent!.Function, Weight: e.EvaluatePremiseWeight(facts, negation, tNorm, tConorm)))
            .MaxBy(e => e.Weight);
        if (tuple.Weight == 0)
            return null;
        var (function, weight) = tuple;
        return method == Mamdani ? function.AlphaCutLeft(weight) : function.PeakLeft();
    }
}
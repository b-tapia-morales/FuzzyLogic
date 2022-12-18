using FuzzyLogic.Rule;

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class FirstOfMaxima : IDefuzzifier
{
    public double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts)
    {
        if (!rules.Any(e => e.IsApplicable(facts))) throw new InapplicableRulesException();
        var tuple = rules
            .Select(e => (Function: e.Consequent!.Function, Weight: e.EvaluatePremiseWeight(facts).GetValueOrDefault()))
            .MaxBy(e => e.Weight);
        if (tuple.Weight == 0)
            return null;
        var (function, weight) = tuple;
        return function.LambdaCutInterval(weight).X1;
    }
}
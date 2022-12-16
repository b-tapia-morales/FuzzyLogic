using FuzzyLogic.Rule;

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class LastOfMaxima : IDefuzzifier
{
    public double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts)
    {
        var tuple = rules
            .Select(e => (Function: e.Consequent!.Function, Weight: e.EvaluatePremiseWeight(facts).GetValueOrDefault()))
            .MaxBy(e => e.Weight);
        var (function, weight) = tuple;
        return function.LambdaCutInterval(weight).X2;
    }
}
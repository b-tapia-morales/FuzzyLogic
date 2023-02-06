using FuzzyLogic.Function.Implication;
using FuzzyLogic.Number;
using FuzzyLogic.Rule;
using static FuzzyLogic.Function.Implication.InferenceMethod;

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class FirstOfMaxima<T> : IDefuzzifier<T> where T : struct, IFuzzyNumber<T>
{
    public double? Defuzzify(ICollection<IRule<T>> rules, IDictionary<string, double> facts,
        InferenceMethod method = Mamdani)
    {
        IDefuzzifier<T>.RulesCheck(rules, facts);
        var tuple = rules
            .Select(e => (e.Consequent!.Function, Weight: e.EvaluatePremiseWeight(facts)))
            .MaxBy(e => e.Weight);
        if (tuple.Weight == 0)
            return null;
        var (function, weight) = tuple;
        return (function as IFuzzyInference)?.LambdaCutInterval(weight, method).GetValueOrDefault().X1;
    }
}
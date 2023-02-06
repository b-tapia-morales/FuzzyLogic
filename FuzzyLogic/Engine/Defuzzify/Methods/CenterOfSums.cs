using FuzzyLogic.Function.Implication;
using FuzzyLogic.Number;
using FuzzyLogic.Rule;
using static FuzzyLogic.Function.Implication.InferenceMethod;

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class CenterOfSums<T> : IDefuzzifier<T> where T : struct, IFuzzyNumber<T>
{
    public double? Defuzzify(ICollection<IRule<T>> rules, IDictionary<string, double> facts,
        InferenceMethod method = Mamdani)
    {
        IDefuzzifier<T>.RulesCheck(rules, facts);
        var tuples = rules
            .Select(e => (Area: e.CalculateArea(facts, method).GetValueOrDefault(),
                Centroid: e.CalculateCentroid(facts).GetValueOrDefault().X))
            .ToList();
        return tuples.All(e => e.Area == 0) ? null : tuples.Sum(e => e.Area * e.Centroid) / tuples.Sum(e => e.Area);
    }
}
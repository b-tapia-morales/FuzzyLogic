using FuzzyLogic.Function.Implication;
using FuzzyLogic.Number;
using FuzzyLogic.Rule;
using static FuzzyLogic.Function.Implication.InferenceMethod;

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class CenterOfLargestArea<T> : IDefuzzifier<T> where T : struct, IFuzzyNumber<T>
{
    public double? Defuzzify(ICollection<IRule<T>> rules, IDictionary<string, double> facts,
        InferenceMethod method = Mamdani)
    {
        IDefuzzifier<T>.RulesCheck(rules, facts);
        var tuple = rules
            .Select(e => (Area: e.CalculateArea(facts).GetValueOrDefault(),
                Centroid: e.CalculateCentroid(facts).GetValueOrDefault().X))
            .MaxBy(e => e.Area);
        return tuple.Area == 0 ? null : tuple.Centroid;
    }
}
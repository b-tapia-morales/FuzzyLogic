using FuzzyLogic.Rule;
using static FuzzyLogic.Engine.Defuzzify.IDefuzzifier;

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class CentreOfSums : IDefuzzifier
{
    public double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts)
    {
        RulesCheck(rules, facts);
        var tuples = rules
            .Select(e => (Area: e.CalculateArea(facts).GetValueOrDefault(),
                Centroid: e.CalculateCentroid(facts).GetValueOrDefault().X))
            .ToList();
        if (tuples.All(e => e.Area == 0))
            return null;
        return tuples.Sum(e => e.Area * e.Centroid) / tuples.Sum(e => e.Area);
    }
}
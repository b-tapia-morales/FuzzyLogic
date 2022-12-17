using FuzzyLogic.Rule;

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class CentreOfSums : IDefuzzifier
{
    public double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts)
    {
        if (!rules.Any(e => e.IsApplicable(facts))) throw new InapplicableRulesException();
        var tuples = rules
            .Select(e => (Area: e.CalculateArea(facts).GetValueOrDefault(),
                Centroid: e.CalculateCentroid(facts).GetValueOrDefault().X))
            .ToList();
        if (tuples.All(e => e.Area == 0)) throw new DefuzzifyException();
        return tuples.Sum(e => e.Area * e.Centroid) / tuples.Sum(e => e.Area);
    }
}
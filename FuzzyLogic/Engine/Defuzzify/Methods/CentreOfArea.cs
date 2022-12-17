using FuzzyLogic.Rule;

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class CentreOfArea : IDefuzzifier
{
    public double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts)
    {
        if (!rules.Any(e => e.IsApplicable(facts))) throw new InapplicableRulesException();
        var tuple = rules
            .Select(e => (Area: e.CalculateArea(facts).GetValueOrDefault(),
                Centroid: e.CalculateCentroid(facts).GetValueOrDefault().X))
            .MaxBy(e => e.Area);
        if (tuple.Area == 0) throw new DefuzzifyException();
        return tuple.Centroid;
    }
}
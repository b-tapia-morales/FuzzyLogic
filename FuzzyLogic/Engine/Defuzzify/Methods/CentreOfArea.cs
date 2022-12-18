using FuzzyLogic.Rule;
using static FuzzyLogic.Engine.Defuzzify.IDefuzzifier;

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class CentreOfArea : IDefuzzifier
{
    public double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts)
    {
        RulesCheck(rules, facts);
        var tuple = rules
            .Select(e => (Area: e.CalculateArea(facts).GetValueOrDefault(),
                Centroid: e.CalculateCentroid(facts).GetValueOrDefault().X))
            .MaxBy(e => e.Area);
        if (tuple.Area == 0) 
            return null;
        return tuple.Centroid;
    }
}
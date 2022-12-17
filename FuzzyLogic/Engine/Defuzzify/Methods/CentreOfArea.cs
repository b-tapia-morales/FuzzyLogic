using FuzzyLogic.Function.Interface;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class CentreOfArea : IDefuzzifier
{
    public double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts)
    {
        var tuple = rules
            .Select(e => (Function: e.Consequent!.Function, Area: e.CalculateArea(facts).GetValueOrDefault()))
            .MaxBy(e => e.Area);
        var function = (IClosedSurface) tuple.Function;
        return function.CalculateCentroid().X0;
    }
}
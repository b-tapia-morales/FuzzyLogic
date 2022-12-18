using FuzzyLogic.Rule;

namespace FuzzyLogic.Engine.Defuzzify;

public interface IDefuzzifier
{
    double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts);
}
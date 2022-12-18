using FuzzyLogic.Rule;
using static System.StringComparison;

namespace FuzzyLogic.Engine.Defuzzify;

public interface IDefuzzifier
{
    static void RulesCheck(ICollection<IRule> rules, IDictionary<string, double> facts)
    {
        if (!rules.Any())
            throw new ArgumentException("The list of rules provided as a parameter contains no elements");
        if (!facts.Any())
            throw new ArgumentException("The dictionary of facts provided as a parameter contains no elements");
        if (!rules.Any(e => e.IsApplicable(facts)))
            throw new InapplicableRulesException();
        var firstConsequent = rules.First().Consequent!.LinguisticVariable.Name;
        if (!rules.Select(e => e.Consequent!.LinguisticVariable.Name)
                .All(e => string.Equals(e, firstConsequent, InvariantCultureIgnoreCase)))
            throw new MismatchedConsequentException();
    }

    double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts);
}
using FuzzyLogic.Enum.Family;
using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Rule;
using static System.StringComparison;
using static FuzzyLogic.Engine.Defuzzify.ImplicationMethod;

namespace FuzzyLogic.Engine.Defuzzify;

public interface IDefuzzifier
{
    protected static void RulesCheck(ICollection<IRule> rules, IDictionary<string, double> facts)
    {
        if (rules.Count == 0)
            throw new ArgumentException("The list of rules provided as a parameter contains no elements");
        if (facts.Count == 0)
            throw new ArgumentException("The dictionary of facts provided as a parameter contains no elements");
        if (!rules.Any(e => e.IsApplicable(facts)))
            throw new InapplicableRulesException();
        var firstConsequent = rules.First().Consequent!.VariableName;
        if (!rules.Select(e => e.Consequent!.VariableName)
                .All(e => string.Equals(e, firstConsequent, InvariantCultureIgnoreCase)))
            throw new MismatchedConsequentException();
    }

    double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts,
        ImplicationMethod method = Mamdani) =>
        Defuzzify(rules, facts, Negation.Standard, Norm.Minimum, Conorm.Maximum, method);

    double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts,
        IOperatorFamily family, ImplicationMethod method = Mamdani) =>
        Defuzzify(rules, facts, family.Negation, family.Norm, family.Conorm, method);

    double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts,
        INegation negation, INorm tNorm, IConorm tConorm, ImplicationMethod method = Mamdani);
}
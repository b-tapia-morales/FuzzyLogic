using FuzzyLogic.Proposition;
using FuzzyLogic.Proposition.Enums;

namespace FuzzyLogic.Rule;

public class FuzzyRule : IRule
{
    public IProposition? Antecedent { get; set; }
    public ICollection<IProposition> Connectives { get; } = new List<IProposition>();
    public IProposition? Consequent { get; set; }
    public bool IsFinalized { get; set; } = false;

    public override string ToString() =>
        $"{Antecedent} {string.Join(' ', Connectives)} {Consequent}";

    public IRule If(IProposition proposition) => If(this, proposition);

    public IRule And(IProposition proposition) => And(this, proposition);

    public IRule Or(IProposition proposition) => Or(this, proposition);

    public IRule Then(IProposition proposition) => Then(this, proposition);

    public static IRule Create() => new FuzzyRule();

    private static IRule If(IRule rule, IProposition proposition)
    {
        if (rule.IsFinalized) throw new FinalizedRuleException();

        if (rule.Antecedent != null) throw new DuplicatedAntecedentException();

        proposition.Connective = Connective.If;
        rule.Antecedent = proposition;
        return rule;
    }

    private static IRule And(IRule rule, IProposition proposition)
    {
        if (rule.IsFinalized) throw new FinalizedRuleException();

        if (rule.Antecedent == null) throw new MissingAntecedentException();

        proposition.Connective = Connective.And;
        rule.Connectives.Add(proposition);
        return rule;
    }

    private static IRule Or(IRule rule, IProposition proposition)
    {
        if (rule.IsFinalized) throw new FinalizedRuleException();

        if (rule.Antecedent == null) throw new MissingAntecedentException();

        proposition.Connective = Connective.Or;
        rule.Connectives.Add(proposition);
        return rule;
    }

    public static IRule Then(IRule rule, IProposition proposition)
    {
        if (rule.Antecedent == null) throw new MissingAntecedentException();

        proposition.Connective = Connective.Then;
        rule.Consequent = proposition;
        rule.IsFinalized = true;
        return rule;
    }
}
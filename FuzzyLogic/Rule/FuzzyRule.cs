using FuzzyLogic.Clause;
using FuzzyLogic.Condition;

namespace FuzzyLogic.Rule;

public class FuzzyRule: IRule
{
    public ICondition? Antecedent { get; set; }
    public ICollection<ICondition> Connectives { get; } = new List<ICondition>();
    public ICondition? Consequent { get; set; }

    public override string ToString() =>
        $"{Antecedent} {string.Join(' ', Connectives)} {Consequent}";

    public IRule If(ICondition condition) => If(this, condition);

    public IRule And(ICondition condition) => And(this, condition);

    public IRule Or(ICondition condition) => Or(this, condition);

    public IRule Then(ICondition condition) => Then(this, condition);

    public static IRule Create() => new FuzzyRule();

    public static IRule If(IRule rule, ICondition condition)
    {
        if (rule.Antecedent != null)
            throw new InvalidOperationException();

        condition.Connective = Connective.If;
        rule.Antecedent = condition;
        return rule;
    }

    public static IRule And(FuzzyRule rule, ICondition condition)
    {
        if (rule.Antecedent == null)
            throw new InvalidOperationException();

        condition.Connective = Connective.And;
        rule.Connectives.Add(condition);
        return rule;
    }

    public static IRule Or(FuzzyRule rule, ICondition condition)
    {
        if (rule.Antecedent == null)
            throw new InvalidOperationException();

        condition.Connective = Connective.Or;
        rule.Connectives.Add(condition);
        return rule;
    }

    public static IRule Then(IRule rule, ICondition condition)
    {
        if (rule.Antecedent == null)
            throw new InvalidOperationException();

        condition.Connective = Connective.Then;
        rule.Consequent = condition;
        return rule;
    }
}
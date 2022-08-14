using FuzzyLogic.Clause;
using FuzzyLogic.Condition;

namespace FuzzyLogic.Rule;

public class FuzzyRule: IRule
{
    public IClause? Antecedent { get; set; }
    public ICollection<IClause> Connectives { get; } = new List<IClause>();
    public IClause? Consequent { get; set; }

    public override string ToString() =>
        $"{Antecedent} {string.Join(' ', Connectives)} {Consequent}";

    public IRule If(ICondition condition) => If(this, condition);

    public IRule And(ICondition condition) => And(this, condition);

    public IRule Or(ICondition condition) => Or(this, condition);

    public IRule Then(ICondition condition) => Then(this, condition);

    public static IRule Initialize() => new FuzzyRule();

    public static IRule If(IRule rule, ICondition condition)
    {
        if (rule.Antecedent != null)
            throw new InvalidOperationException();

        rule.Antecedent = FuzzyClause.If(condition);
        return rule;
    }

    public static IRule And(FuzzyRule rule, ICondition condition)
    {
        if (rule.Antecedent == null)
            throw new InvalidOperationException();

        rule.Connectives.Add(FuzzyClause.And(condition));
        return rule;
    }

    public static IRule Or(FuzzyRule rule, ICondition condition)
    {
        if (rule.Antecedent == null)
            throw new InvalidOperationException();

        rule.Connectives.Add(FuzzyClause.Or(condition));
        return rule;
    }

    public static IRule Then(IRule rule, ICondition condition)
    {
        if (rule.Antecedent == null)
            throw new InvalidOperationException();

        rule.Consequent = FuzzyClause.Then(condition);
        return rule;
    }
}
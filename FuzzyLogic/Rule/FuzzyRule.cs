using FuzzyLogic.Clause;
using FuzzyLogic.Condition;

namespace FuzzyLogic.Rule;

public class FuzzyRule
{
    public FuzzyClause? Antecedent { get; set; }
    public ICollection<FuzzyClause> Connectives { get; } = new List<FuzzyClause>();
    public FuzzyClause? Consequent { get; set; }

    public override string ToString() =>
        $"{Antecedent} {string.Join(' ', Connectives)} {Consequent}";
    
    public FuzzyRule If(FuzzyCondition condition) => If(this, condition);

    public FuzzyRule And(FuzzyCondition condition) => And(this, condition);

    public FuzzyRule Or(FuzzyCondition condition) => Or(this, condition);

    public FuzzyRule Then(FuzzyCondition condition) => Then(this, condition);
    
    public static FuzzyRule Initialize() => new();
    
    public static FuzzyRule If(FuzzyRule rule, FuzzyCondition condition)
    {
        if (rule.Antecedent != null)
            throw new InvalidOperationException();

        rule.Antecedent = FuzzyClause.If(condition);
        return rule;
    }

    public static FuzzyRule And(FuzzyRule rule, FuzzyCondition condition)
    {
        if (rule.Antecedent == null)
            throw new InvalidOperationException();

        rule.Connectives.Add(FuzzyClause.And(condition));
        return rule;
    }

    public static FuzzyRule Or(FuzzyRule rule, FuzzyCondition condition)
    {
        if (rule.Antecedent == null)
            throw new InvalidOperationException();

        rule.Connectives.Add(FuzzyClause.Or(condition));
        return rule;
    }

    public static FuzzyRule Then(FuzzyRule rule, FuzzyCondition condition)
    {
        if (rule.Antecedent == null)
            throw new InvalidOperationException();

        rule.Consequent = FuzzyClause.Then(condition);
        return rule;
    }
}
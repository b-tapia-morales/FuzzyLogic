using FuzzyLogic.Condition;

namespace FuzzyLogic.Clause;

public class FuzzyClause
{
    public FuzzyClause(Connective connective, FuzzyCondition condition)
    {
        Connective = connective;
        Condition = condition;
    }

    public Connective Connective { get; }
    public FuzzyCondition Condition { get; }

    public override string ToString() => $"{Connective} {Condition}";

    public static FuzzyClause If(FuzzyCondition condition) => new(Connective.If, condition);

    public static FuzzyClause And(FuzzyCondition condition) => new(Connective.And, condition);

    public static FuzzyClause Or(FuzzyCondition condition) => new(Connective.Or, condition);

    public static FuzzyClause Then(FuzzyCondition condition) => new(Connective.Then, condition);
}
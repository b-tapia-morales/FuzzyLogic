using FuzzyLogic.Condition;

namespace FuzzyLogic.Clause;

public class FuzzyClause: IClause
{
    public FuzzyClause(Connective connective, ICondition condition)
    {
        Connective = connective;
        Condition = condition;
    }

    public Connective Connective { get; }
    public ICondition Condition { get; }

    public override string ToString() => $"{Connective} {Condition}";

    public static FuzzyClause If(ICondition condition) => new(Connective.If, condition);

    public static FuzzyClause And(ICondition condition) => new(Connective.And, condition);

    public static FuzzyClause Or(ICondition condition) => new(Connective.Or, condition);

    public static FuzzyClause Then(ICondition condition) => new(Connective.Then, condition);
}
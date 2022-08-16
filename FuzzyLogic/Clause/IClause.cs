using FuzzyLogic.Condition;

namespace FuzzyLogic.Clause;

public interface IClause
{
    public Connective Connective { get; }
    public ICondition Condition { get; }

    string RetrieveLinguisticVariable();
}
using FuzzyLogic.Clause;
using FuzzyLogic.Condition;

namespace FuzzyLogic.Rule;

public interface IRule
{
    public IClause? Antecedent { get; set; }
    public ICollection<IClause> Connectives { get; }
    public IClause? Consequent { get; set; }

    IRule If(ICondition condition);

    IRule And(ICondition condition);

    IRule Or(ICondition condition);

    IRule Then(ICondition condition);
}
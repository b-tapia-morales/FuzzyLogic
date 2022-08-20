using FuzzyLogic.Clause;
using FuzzyLogic.Condition;

namespace FuzzyLogic.Rule;

public interface IRule
{
    public ICondition? Antecedent { get; set; }
    public ICollection<ICondition> Connectives { get; }
    public ICondition? Consequent { get; set; }

    IRule If(ICondition condition);

    IRule And(ICondition condition);

    IRule Or(ICondition condition);

    IRule Then(ICondition condition);
}
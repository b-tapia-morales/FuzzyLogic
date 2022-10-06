using FuzzyLogic.Linguistics;
using FuzzyLogic.MembershipFunctions.Real;

namespace FuzzyLogic.Condition;

public interface ICondition
{
    public Connective Connective { get; set; }
    public IVariable LinguisticVariable { get; set; }
    public Literal Literal { get; set; }
    public LinguisticHedge LinguisticHedge { get; set; }
    public IRealFunction Function { get; set; }
}
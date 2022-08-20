using FuzzyLogic.Linguistics;
using FuzzyLogic.MembershipFunctions.Real;

namespace FuzzyLogic.Condition;

public interface ICondition
{
    public Connective? Connective { get; set; }
    public LinguisticVariable LinguisticVariable { get; set; }
    public Literal Literal { get; set; }
    public IRealFunction Function { get; set; }
}
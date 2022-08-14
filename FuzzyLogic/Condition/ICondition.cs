using FuzzyLogic.Linguistics;
using FuzzyLogic.MembershipFunctions.Real;

namespace FuzzyLogic.Condition;

public interface ICondition
{
    public Literal Literal { get; }
    public LinguisticVariable LinguisticVariable { get; }
    public IRealFunction Function { get; }
}
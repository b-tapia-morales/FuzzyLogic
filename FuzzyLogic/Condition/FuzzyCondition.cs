using FuzzyLogic.Linguistics;
using FuzzyLogic.MembershipFunctions.Real;

namespace FuzzyLogic.Condition;

public class FuzzyCondition : ICondition
{
    public FuzzyCondition(LiteralToken token, LinguisticVariable linguisticVariable, IRealFunction function)
    {
        Literal = Literal.FromToken(token);
        LinguisticVariable = linguisticVariable;
        Function = function;
    }

    public Literal Literal { get; }
    public LinguisticVariable LinguisticVariable { get; }
    public IRealFunction Function { get; }

    public override string ToString() => $"{LinguisticVariable} {Literal.ReadableName} {Function.Name}";
}
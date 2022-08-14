using FuzzyLogic.Linguistics;
using FuzzyLogic.MembershipFunctions.Real;

namespace FuzzyLogic.Condition;

public class FuzzyCondition: ICondition
{
    public Literal Literal { get; }
    public LinguisticVariable LinguisticVariable { get; }
    public IRealFunction Function { get; }

    public FuzzyCondition(LiteralToken literal, LinguisticVariable linguisticVariable, IRealFunction function)
    {
        Literal = Literal.ReadOnlyDictionary[literal];
        LinguisticVariable = linguisticVariable;
        Function = function;
    }

    public override string ToString() => $"{LinguisticVariable} {Literal.ReadableName} {Function.Name}";
}
using FuzzyLogic.Linguistics;
using FuzzyLogic.MembershipFunctions.Real;

namespace FuzzyLogic.Condition;

public class FuzzyCondition : ICondition
{
    public FuzzyCondition(LinguisticVariable linguisticVariable, LiteralToken token, IRealFunction function)
    {
        LinguisticVariable = linguisticVariable;
        Literal = Literal.FromToken(token);
        Function = function;
    }

    public FuzzyCondition(Connective connective, LinguisticVariable linguisticVariable, LiteralToken token,
        IRealFunction function)
    {
        Connective = connective;
        LinguisticVariable = linguisticVariable;
        Literal = Literal.FromToken(token);
        Function = function;
    }

    public Connective? Connective { get; set; }
    public Literal Literal { get; set; }
    public LinguisticVariable LinguisticVariable { get; set; }
    public IRealFunction Function { get; set; }

    public override string ToString() => $"{Connective} {LinguisticVariable} {Literal.ReadableName} {Function.Name}";
}
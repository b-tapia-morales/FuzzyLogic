using FuzzyLogic.Linguistics;
using FuzzyLogic.MembershipFunctions.Real;

namespace FuzzyLogic.Condition;

public class FuzzyCondition : ICondition
{
    public FuzzyCondition(IVariable linguisticVariable, LiteralToken token, IRealFunction function)
    {
        LinguisticVariable = linguisticVariable;
        Literal = Literal.FromToken(token);
        Function = function;
    }

    public FuzzyCondition(IVariable linguisticVariable, LiteralToken literalToken, HedgeToken hedgeToken,
        IRealFunction function)
    {
        LinguisticVariable = linguisticVariable;
        Literal = Literal.FromToken(literalToken);
        LinguisticHedge = LinguisticHedge.FromToken(hedgeToken);
        Function = function;
    }

    public Connective Connective { get; set; } = Connective.None;
    public IVariable LinguisticVariable { get; set; }
    public Literal Literal { get; set; }
    public LinguisticHedge LinguisticHedge { get; set; } = LinguisticHedge.None;
    public IRealFunction Function { get; set; }

    public override string ToString()
    {
        var connective = Connective != Connective.None ? $"{Connective} " : string.Empty;
        var linguisticHedge = LinguisticHedge != LinguisticHedge.None ? $"{LinguisticHedge} " : string.Empty;
        return $"{connective}{LinguisticVariable} {Literal.ReadableName} {linguisticHedge}{Function.Name}";
    }
}
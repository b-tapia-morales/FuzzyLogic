using FuzzyLogic.Function.Interface;
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition.Enums;
using FuzzyLogic.Variable;

namespace FuzzyLogic.Proposition;

public class FuzzyProposition : IProposition
{
    public FuzzyProposition(IVariable linguisticVariable, LiteralToken literalToken, HedgeToken hedgeToken,
        IRealFunction function)
    {
        Connective = Connective.None;
        LinguisticVariable = linguisticVariable;
        Literal = Literal.FromToken(literalToken);
        LinguisticHedge = LinguisticHedge.FromToken(hedgeToken);
        Function = function;
    }

    public Connective Connective { get; set; }
    public IVariable LinguisticVariable { get; }
    public Literal Literal { get; }
    public LinguisticHedge LinguisticHedge { get; }
    public IRealFunction Function { get; }

    public FuzzyNumber ApplyUnaryOperators(double crispNumber)
    {
        var membershipFunction = Function.SimpleFunction();
        var hedgeFunction = LinguisticHedge.Function;
        var literalFunction = Literal.Function!;
        return literalFunction(hedgeFunction(membershipFunction(crispNumber)));
    }

    public override string ToString()
    {
        var connective = Connective != Connective.None ? $"{Connective} " : string.Empty;
        var linguisticHedge = LinguisticHedge != LinguisticHedge.None ? $"{LinguisticHedge} " : string.Empty;
        return $"{connective}{LinguisticVariable} {Literal.ReadableName} {linguisticHedge}{Function.Name}";
    }

    public static IProposition Is(ILinguisticBase @base, string variableName, string entryName,
        HedgeToken hedgeToken = HedgeToken.None)
    {
        var variable = @base.Retrieve(variableName) ??
                       throw new VariableNotFoundException(variableName);
        var entry = variable.RetrieveLinguisticEntry(entryName) ??
                    throw new EntryNotFoundException(variableName, entryName);
        return new FuzzyProposition(variable, LiteralToken.Affirmation, hedgeToken, entry);
    }

    public static IProposition IsNot(ILinguisticBase @base, string variableName, string entryName,
        HedgeToken hedgeToken = HedgeToken.None)
    {
        var variable = @base.Retrieve(variableName) ??
                       throw new VariableNotFoundException(variableName);
        var entry = variable.RetrieveLinguisticEntry(entryName) ??
                    throw new EntryNotFoundException(variableName, entryName);
        return new FuzzyProposition(variable, LiteralToken.Negation, hedgeToken, entry);
    }
}
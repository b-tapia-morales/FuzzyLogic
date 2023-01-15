using FuzzyLogic.Function.Interface;
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition.Enums;
using FuzzyLogic.Variable;

namespace FuzzyLogic.Proposition;

public class FuzzyProposition : IProposition
{
    private FuzzyProposition(IVariable linguisticVariable, LiteralToken literalToken, IRealFunction function,
        HedgeToken hedgeToken = HedgeToken.None, ConnectiveToken connectiveToken = ConnectiveToken.None)
    {
        Connective = Connective.FromToken(connectiveToken);
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
        return $"{connective}{LinguisticVariable.Name} {Literal.ReadableName} {linguisticHedge}{Function.Name}";
    }

    public static IProposition Is(ILinguisticBase linguisticBase, string variableName, string entryName,
        HedgeToken hedgeToken = HedgeToken.None) =>
        Create(linguisticBase, variableName, LiteralToken.Affirmation, hedgeToken, entryName);

    public static IProposition IsNot(ILinguisticBase linguisticBase, string variableName, string entryName,
        HedgeToken hedgeToken = HedgeToken.None) =>
        Create(linguisticBase, variableName, LiteralToken.Negation, hedgeToken, entryName);

    private static IProposition Create(ILinguisticBase linguisticBase, string variableName, LiteralToken literalToken,
        HedgeToken hedgeToken, string entryName)
    {
        var variable = linguisticBase.Retrieve(variableName) ??
                       throw new VariableNotFoundException(variableName);
        var entry = variable.RetrieveLinguisticEntry(entryName) ??
                    throw new EntryNotFoundException(variableName, entryName);
        return new FuzzyProposition(variable, literalToken, entry, hedgeToken);
    }
}
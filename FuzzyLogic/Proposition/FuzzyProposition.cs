using FuzzyLogic.Function.Interface;
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition.Enums;
using FuzzyLogic.Variable;

namespace FuzzyLogic.Proposition;

public class FuzzyProposition<T> : IProposition<T> where T : struct, IFuzzyNumber<T>
{
    private FuzzyProposition(IVariable linguisticVariable, LiteralToken literalToken,
        IMembershipFunction<double> function, HedgeToken hedgeToken = HedgeToken.None,
        ConnectiveToken connectiveToken = ConnectiveToken.None)
    {
        Connective = Connective<T>.FromToken(connectiveToken);
        LinguisticVariable = linguisticVariable;
        Literal = Literal<T>.FromToken(literalToken);
        LinguisticHedge = LinguisticHedge<T>.FromToken(hedgeToken);
        Function = function;
    }

    public Connective<T> Connective { get; set; }
    public IVariable LinguisticVariable { get; }
    public Literal<T> Literal { get; }
    public LinguisticHedge<T> LinguisticHedge { get; }
    public IMembershipFunction<double> Function { get; }

    public bool IsApplicable(IDictionary<string, double> facts) => facts.ContainsKey(LinguisticVariable.Name);

    public T ApplyUnaryOperators(double crispNumber)
    {
        var membershipFunction = Function.AsFunction();
        var hedgeFunction = LinguisticHedge.Function;
        var literalFunction = Literal.Function!;
        return literalFunction(hedgeFunction(membershipFunction(crispNumber)));
    }

    public override string ToString()
    {
        var connective = Connective != Connective<T>.None ? $"{Connective} " : string.Empty;
        var linguisticHedge = LinguisticHedge != LinguisticHedge<T>.None ? $"{LinguisticHedge} " : string.Empty;
        return $"{connective}{LinguisticVariable.Name} {Literal.ReadableName} {linguisticHedge}{Function.Name}";
    }

    public static IProposition<T> Is(ILinguisticBase linguisticBase, string variableName, string entryName,
        HedgeToken hedgeToken = HedgeToken.None) =>
        Create(linguisticBase, variableName, LiteralToken.Affirmation, hedgeToken, entryName);

    public static IProposition<T> IsNot(ILinguisticBase linguisticBase, string variableName, string entryName,
        HedgeToken hedgeToken = HedgeToken.None) =>
        Create(linguisticBase, variableName, LiteralToken.Negation, hedgeToken, entryName);

    private static IProposition<T> Create(ILinguisticBase linguisticBase, string variableName,
        LiteralToken literalToken, HedgeToken hedgeToken, string entryName)
    {
        var variable = linguisticBase.Retrieve(variableName) ??
                       throw new VariableNotFoundException(variableName);
        var entry = variable.RetrieveLinguisticEntry(entryName) ??
                    throw new EntryNotFoundException(variableName, entryName);
        return new FuzzyProposition<T>(variable, literalToken, entry, hedgeToken);
    }
}
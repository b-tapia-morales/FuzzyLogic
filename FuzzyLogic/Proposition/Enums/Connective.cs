using Ardalis.SmartEnum;
using FuzzyLogic.Number;

namespace FuzzyLogic.Proposition.Enums;

public class Connective : SmartEnum<Connective>
{
    public static readonly Connective None =
        new(nameof(None), string.Empty, null, (int) ConnectiveToken.None);

    public static readonly Connective If =
        new(nameof(If), "IF", null, (int) ConnectiveToken.Antecedent);

    public static readonly Connective Then =
        new(nameof(Then), "THEN", null, (int) ConnectiveToken.Consequent);

    public static readonly Connective And =
        new(nameof(And), "AND", (a, b) => a & b, (int) ConnectiveToken.Conjunction);

    public static readonly Connective Or =
        new(nameof(Or), "OR", (a, b) => a | b, (int) ConnectiveToken.Disjunction);

    private static readonly Dictionary<ConnectiveToken, Connective> TokenDictionary = new()
    {
        {ConnectiveToken.None, None},
        {ConnectiveToken.Antecedent, If},
        {ConnectiveToken.Consequent, Then},
        {ConnectiveToken.Conjunction, And},
        {ConnectiveToken.Disjunction, Or}
    };

    private static readonly Dictionary<string, Connective> ReadableNameDictionary =
        TokenDictionary.ToDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    public Connective(string name, string readableName, Func<FuzzyNumber, FuzzyNumber, FuzzyNumber>? function,
        int value) : base(name, value)
    {
        ReadableName = readableName;
        Function = function;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, FuzzyNumber, FuzzyNumber>? Function { get; }

    public static Connective FromToken(ConnectiveToken token) => TokenDictionary[token];

    public static Connective FromReadableName(string readableName) => ReadableNameDictionary[readableName];

    public override string ToString() => ReadableName;
}

public enum ConnectiveToken
{
    None = 0,
    Antecedent = 1,
    Consequent = 2,
    Conjunction = 3,
    Disjunction = 4
}
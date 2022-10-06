using Ardalis.SmartEnum;

namespace FuzzyLogic.Condition;

public class Connective : SmartEnum<Connective>
{
    public static readonly Connective None = new(nameof(None), string.Empty, ConnectiveToken.None,
        (int) ConnectiveToken.None);

    public static readonly Connective If = new(nameof(If), "IF", ConnectiveToken.Antecedent,
        (int) ConnectiveToken.Antecedent);

    public static readonly Connective Then = new(nameof(Then), "THEN", ConnectiveToken.Consequent,
        (int) ConnectiveToken.Consequent);

    public static readonly Connective And = new(nameof(And), "AND", ConnectiveToken.Conjunction,
        (int) ConnectiveToken.Conjunction);

    public static readonly Connective Or = new(nameof(Or), "OR", ConnectiveToken.Disjunction,
        (int) ConnectiveToken.Disjunction);

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

    public Connective(string name, string readableName, ConnectiveToken token, int value) : base(name, value)
    {
        ReadableName = readableName;
        Token = token;
    }

    public string ReadableName { get; }
    public ConnectiveToken Token { get; }

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
using Ardalis.SmartEnum;
using static FuzzyLogic.Clause.ConnectiveToken;

namespace FuzzyLogic.Clause;

public class Connective : SmartEnum<Connective>
{
    public static readonly Connective If = new(nameof(If), "IF", Antecedent, (int) Antecedent);
    public static readonly Connective Then = new(nameof(Then), "THEN", Consequent, (int) Consequent);
    public static readonly Connective And = new(nameof(And), "AND", Conjunction, (int) Conjunction);
    public static readonly Connective Or = new(nameof(Or), "OR", Disjunction, (int) Disjunction);

    private static readonly Dictionary<ConnectiveToken, Connective> TokenDictionary = new()
    {
        {Antecedent, If},
        {Consequent, Then},
        {Conjunction, And},
        {Disjunction, Or}
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
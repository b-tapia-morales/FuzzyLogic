using Ardalis.SmartEnum;
using static FuzzyLogic.Condition.LiteralToken;

namespace FuzzyLogic.Condition;

public class Literal : SmartEnum<Literal>
{
    public static readonly Literal Is = new(nameof(Is), "IS", Affirmation, (int) Affirmation);
    public static readonly Literal IsNot = new(nameof(IsNot), "IS NOT", Negation, (int) Negation);

    private static readonly Dictionary<LiteralToken, Literal> TokenDictionary = new()
    {
        {Affirmation, Is},
        {Negation, IsNot}
    };

    private static readonly Dictionary<string, Literal> ReadableNameDictionary =
        TokenDictionary.ToDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    public Literal(string name, string readableName, LiteralToken token, int value) : base(name, value)
    {
        ReadableName = readableName;
        Token = token;
    }

    public string ReadableName { get; }
    public LiteralToken Token { get; }

    public static Literal FromToken(LiteralToken token) => TokenDictionary[token];

    public static Literal FromReadableName(string readableName) => ReadableNameDictionary[readableName];

    public override string ToString() => ReadableName;
}
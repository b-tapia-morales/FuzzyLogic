using Ardalis.SmartEnum;
using FuzzyLogic.Number;

namespace FuzzyLogic.Proposition.Enums;

public class Literal : SmartEnum<Literal>
{
    public static readonly Literal None =
        new(nameof(None), string.Empty, null, (int) LiteralToken.None);

    public static readonly Literal Is =
        new(nameof(Is), "IS", y => y, (int) LiteralToken.Affirmation);

    public static readonly Literal IsNot =
        new(nameof(IsNot), "IS NOT", y => !y, (int) LiteralToken.Negation);

    private static readonly Dictionary<LiteralToken, Literal> TokenDictionary = new()
    {
        {LiteralToken.None, None},
        {LiteralToken.Affirmation, Is},
        {LiteralToken.Negation, IsNot}
    };

    private static readonly Dictionary<string, Literal> ReadableNameDictionary =
        TokenDictionary.ToDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    public Literal(string name, string readableName, Func<FuzzyNumber, FuzzyNumber>? function,
        int value) : base(name, value)
    {
        ReadableName = readableName;
        Function = function;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, FuzzyNumber>? Function { get; }

    public static Literal FromToken(LiteralToken token) => TokenDictionary[token];

    public static Literal FromReadableName(string readableName) => ReadableNameDictionary[readableName];

    public override string ToString() => ReadableName;
}

public enum LiteralToken
{
    None = 0,
    Affirmation = 1,
    Negation = 2
}
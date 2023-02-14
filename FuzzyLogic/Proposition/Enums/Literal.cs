using Ardalis.SmartEnum;
using FuzzyLogic.Number;

namespace FuzzyLogic.Proposition.Enums;

public class Literal<T> : SmartEnum<Literal<T>> where T : IFuzzyNumber<T>
{
    public static readonly Literal<T> None =
        new(nameof(None), string.Empty, null, (int) LiteralToken.None);

    public static readonly Literal<T> Is =
        new(nameof(Is), "IS", y => y, (int) LiteralToken.Affirmation);

    public static readonly Literal<T> IsNot =
        new(nameof(IsNot), "IS NOT", y => !y, (int) LiteralToken.Negation);

    private static readonly Dictionary<LiteralToken, Literal<T>> TokenDictionary = new()
    {
        {LiteralToken.None, None},
        {LiteralToken.Affirmation, Is},
        {LiteralToken.Negation, IsNot}
    };

    private static readonly Dictionary<string, Literal<T>> ReadableNameDictionary =
        TokenDictionary.ToDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    public Literal(string name, string readableName, Func<T, T>? function,
        int value) : base(name, value)
    {
        ReadableName = readableName;
        Function = function;
    }

    public string ReadableName { get; }
    public Func<T, T>? Function { get; }

    public static Literal<T> FromToken(LiteralToken token) => TokenDictionary[token];

    public static Literal<T> FromReadableName(string readableName) => ReadableNameDictionary[readableName];

    public override string ToString() => ReadableName;
}

public enum LiteralToken
{
    None = 0,
    Affirmation = 1,
    Negation = 2
}
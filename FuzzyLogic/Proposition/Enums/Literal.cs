using Ardalis.SmartEnum;
using FuzzyLogic.Enum;
using FuzzyLogic.Number;

namespace FuzzyLogic.Proposition.Enums;

public class Literal : SmartEnum<Literal>, IEnum<Literal, LiteralToken>
{
    public static readonly Literal None =
        new(nameof(None), string.Empty, null, (int) LiteralToken.None);

    public static readonly Literal Is =
        new(nameof(Is), "IS", y => y, (int) LiteralToken.Affirmation);

    public static readonly Literal IsNot =
        new(nameof(IsNot), "IS NOT", y => !y, (int) LiteralToken.Negation);

    private Literal(string name, string readableName, Func<FuzzyNumber, FuzzyNumber>? function,
        int value) : base(name, value)
    {
        ReadableName = readableName;
        Function = function;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, FuzzyNumber>? Function { get; }
}

public enum LiteralToken
{
    None = 0,
    Affirmation = 1,
    Negation = 2
}
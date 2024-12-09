using Ardalis.SmartEnum;
using FuzzyLogic.Enum;

namespace FuzzyLogic.Proposition.Enums;

public class Literal : SmartEnum<Literal>, IEnum<Literal, LiteralToken>
{
    public static readonly Literal Is =
        new(nameof(Is), "IS", (int) LiteralToken.Affirmation);

    public static readonly Literal IsNot =
        new(nameof(IsNot), "IS NOT", (int) LiteralToken.Negation);

    private Literal(string name, string readableName, int value) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum LiteralToken
{
    Affirmation,
    Negation
}
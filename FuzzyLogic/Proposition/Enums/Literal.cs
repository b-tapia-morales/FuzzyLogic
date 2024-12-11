using Ardalis.SmartEnum;
using FuzzyLogic.Enum;

namespace FuzzyLogic.Proposition.Enums;

public class Literal : SmartEnum<Literal>, IEnum<Literal, LiteralType>
{
    public static readonly Literal Is =
        new(nameof(Is), "IS", (int) LiteralType.Affirmation);

    public static readonly Literal IsNot =
        new(nameof(IsNot), "IS NOT", (int) LiteralType.Negation);

    private Literal(string name, string readableName, int value) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum LiteralType
{
    Affirmation,
    Negation
}
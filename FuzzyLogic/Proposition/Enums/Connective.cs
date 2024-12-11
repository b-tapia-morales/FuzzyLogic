using Ardalis.SmartEnum;
using FuzzyLogic.Enum;

namespace FuzzyLogic.Proposition.Enums;

public class Connective : SmartEnum<Connective>, IEnum<Connective, ConnectiveType>
{
    public static readonly Connective If =
        new(nameof(If), "IF", (int) ConnectiveType.Antecedent);

    public static readonly Connective And =
        new(nameof(And), "AND", (int) ConnectiveType.Conjunction);

    public static readonly Connective Or =
        new(nameof(Or), "OR", (int) ConnectiveType.Disjunction);

    public static readonly Connective Then =
        new(nameof(Then), "THEN", (int) ConnectiveType.Consequent);

    private Connective(string name, string readableName, int value) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum ConnectiveType
{
    Antecedent,
    Consequent,
    Conjunction,
    Disjunction
}
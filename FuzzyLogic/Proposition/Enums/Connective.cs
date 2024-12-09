using Ardalis.SmartEnum;
using FuzzyLogic.Enum;

namespace FuzzyLogic.Proposition.Enums;

public class Connective : SmartEnum<Connective>, IEnum<Connective, ConnectiveToken>
{
    public static readonly Connective If =
        new(nameof(If), "IF", (int) ConnectiveToken.Antecedent);

    public static readonly Connective And =
        new(nameof(And), "AND", (int) ConnectiveToken.Conjunction);

    public static readonly Connective Or =
        new(nameof(Or), "OR", (int) ConnectiveToken.Disjunction);

    public static readonly Connective Then =
        new(nameof(Then), "THEN", (int) ConnectiveToken.Consequent);

    private Connective(string name, string readableName, int value) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum ConnectiveToken
{
    Antecedent = 1,
    Consequent = 2,
    Conjunction = 3,
    Disjunction = 4
}
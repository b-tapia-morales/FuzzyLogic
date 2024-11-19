using Ardalis.SmartEnum;
using FuzzyLogic.Enum;
using FuzzyLogic.Number;

namespace FuzzyLogic.Proposition.Enums;

public class Connective : SmartEnum<Connective>, IEnum<Connective, ConnectiveToken>
{
    public static readonly Connective None =
        new(nameof(None), string.Empty, null, (int) ConnectiveToken.None);

    public static readonly Connective If =
        new(nameof(If), "IF", null, (int) ConnectiveToken.Antecedent);

    public static readonly Connective And =
        new(nameof(And), "AND", (a, b) => a & b, (int) ConnectiveToken.Conjunction);

    public static readonly Connective Or =
        new(nameof(Or), "OR", (a, b) => a | b, (int) ConnectiveToken.Disjunction);

    public static readonly Connective Then =
        new(nameof(Then), "THEN", (a, b) => a >> b, (int) ConnectiveToken.Consequent);

    private Connective(string name, string readableName, Func<FuzzyNumber, FuzzyNumber, FuzzyNumber>? function,
        int value) : base(name, value)
    {
        ReadableName = readableName;
        Function = function;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, FuzzyNumber, FuzzyNumber>? Function { get; }
}

public enum ConnectiveToken
{
    None = 0,
    Antecedent = 1,
    Consequent = 2,
    Conjunction = 3,
    Disjunction = 4
}
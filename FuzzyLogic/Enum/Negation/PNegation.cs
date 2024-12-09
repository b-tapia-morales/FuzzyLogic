using Ardalis.SmartEnum;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Enum.Negation;

public class PNegation(PNegator @operator, double gamma) : INegation
{
    private PNegator Operator { get; } = @operator;
    private double Gamma { get; } = gamma;

    public FuzzyNumber Complement(FuzzyNumber x) => Operator.Function(x, Gamma);
}

public enum PNegatorType
{
    Sugeno,
    Yager
}

public class PNegator : SmartEnum<PNegator>, IEnum<PNegator, PNegatorType>
{
    public static readonly PNegator Sugeno =
        new(nameof(Sugeno), "Sugeno", (a, gamma) => (1 - a) / (1 - gamma * a), (int) PNegatorType.Sugeno);

    public static readonly PNegator Yager =
        new(nameof(Yager), "Yager", (a, gamma) => Pow(1 - Pow(a, gamma), 1.0 / gamma),
            (int) PNegatorType.Yager);

    private PNegator(string name, string readableName, Func<FuzzyNumber, double, FuzzyNumber> function, int value) : base(name, value)
    {
        ReadableName = readableName;
        Function = function;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, double, FuzzyNumber> Function { get; }
}
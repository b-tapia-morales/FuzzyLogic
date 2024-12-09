using Ardalis.SmartEnum;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Enum.Residuum;

public class PResiduum(PResiduumOperator @operator, double omega) : IResiduum
{
    private PResiduumOperator Operator { get; } = @operator;
    private double Omega { get; } = omega;

    public FuzzyNumber Implication(FuzzyNumber x, FuzzyNumber y) => Operator.Function(x, y, Omega);
}

public enum PResiduumType
{
    PseudoLukasiewicz1,
    PseudoLukasiewicz2
}

public class PResiduumOperator : SmartEnum<PResiduumOperator>, IEnum<PResiduumOperator, PResiduumType>
{
    public static readonly PResiduumOperator PseudoLukasiewicz1 =
        new(nameof(PseudoLukasiewicz1), "Pseudo-Łukasiewicz 1", (a, b, omega) => Min(1, (1 - a + (1 - omega) * b) / (1 + omega * a)),
            (int) PResiduumType.PseudoLukasiewicz1);

    public static readonly PResiduumOperator PseudoLukasiewicz2 =
        new(nameof(PseudoLukasiewicz2), "Pseudo-Łukasiewicz 2", (a, b, omega) => Min(1, 1 - Pow(a, omega) + Pow(b, omega)),
            (int) PResiduumType.PseudoLukasiewicz2);

    private PResiduumOperator(string name, string readableName, Func<FuzzyNumber, FuzzyNumber, double, FuzzyNumber> function, int value) : base(name, value)
    {
        ReadableName = readableName;
        Function = function;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, FuzzyNumber, double, FuzzyNumber> Function { get; }
}
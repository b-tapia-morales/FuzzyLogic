using Ardalis.SmartEnum;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Enum.TConorm;

public class PConorm(PIntersector @operator, double beta) : IConorm
{
    private PIntersector Operator { get; } = @operator;
    private double Beta { get; } = beta;

    public FuzzyNumber Union(FuzzyNumber x, FuzzyNumber y) => Operator.Function(x, y, Beta);
}

public enum PIntersectorType
{
    Hamacher,
    SugenoWeber
}

public class PIntersector : SmartEnum<PIntersector>, IEnum<PIntersector, PIntersectorType>
{
    public static readonly PIntersector Hamacher =
        new(nameof(Hamacher), "Hamacher", (x, y, beta) => (x + y + (beta - 1) * x * y) / (1 + beta * x * y),
            (int) PIntersectorType.Hamacher);

    public static readonly PIntersector SugenoWeber =
        new(nameof(SugenoWeber), "Sugeno-Weber", (x, y, beta) => Min(x + y + beta * x * y, 1),
            (int) PIntersectorType.SugenoWeber);

    private PIntersector(string name, string readableName, Func<FuzzyNumber, FuzzyNumber, double, FuzzyNumber> function, int value) : base(name, value)
    {
        ReadableName = readableName;
        Function = function;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, FuzzyNumber, double, FuzzyNumber> Function { get; }
}
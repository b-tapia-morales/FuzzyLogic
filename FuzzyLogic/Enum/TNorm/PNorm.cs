using Ardalis.SmartEnum;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Enum.TNorm;

public class PNorm(PUnitor @operator, double alpha) : INorm
{
    private PUnitor Operator { get; } = @operator;
    private double Alpha { get; } = alpha;

    public FuzzyNumber Intersection(FuzzyNumber x, FuzzyNumber y) => Operator.Function(x, y, Alpha);
}

public enum PUnitorType
{
    Hamacher,
    SugenoWeber,
    SchweizerSklar,
    Frank,
    AczelAlsina,
    Dombi
}

public class PUnitor : SmartEnum<PUnitor>, IEnum<PUnitor, PUnitorType>
{
    private const double Epsilon = 1E-5;

    public static readonly PUnitor Hamacher =
        new(nameof(Hamacher), "Hamacher", (x, y, alpha) => (x * y) / (alpha + (1 - alpha) * (x + y + x * y)),
            (int) PUnitorType.Hamacher);

    public static readonly PUnitor SugenoWeber =
        new(nameof(SugenoWeber), "Sugeno-Weber", (x, y, alpha) => Max((x + y - 1 + alpha * x * y) / (1 + alpha), 0),
            (int) PUnitorType.SugenoWeber);

    public static readonly PUnitor SchweizerSklar =
        new(nameof(SchweizerSklar), "Schweizer-Sklar", (x, y, p) =>
        {
            // p = -∞
            if (double.IsNegativeInfinity(p))
                return Norm.Minimum.Function(x, y);
            // -∞ < p < 0
            if (p is > double.NegativeInfinity and < 0)
                return Pow(Pow(x, p) + Pow(y, p) - 1, 1 / p);
            // p = 0
            if (Abs(p) <= Epsilon)
                return Norm.Product.Function(x, y);
            // 0 <= p < +∞
            if (p is > 0 and < double.PositiveInfinity)
                return Pow(Max(0, Pow(x, p) + Pow(y, p) - 1), 1 / p);
            // p = +∞
            return Norm.Drastic.Function(x, y);
        }, (int) PUnitorType.SchweizerSklar);

    public static readonly PUnitor Frank =
        new(nameof(Frank), "Frank", (x, y, p) =>
        {
            // p = 0
            if (Abs(p) <= Epsilon)
                return Norm.Minimum.Function(x, y);
            // p = 1
            if (Abs(1 - p) <= Epsilon)
                return Norm.Product.Function(x, y);
            // p = +∞
            if (double.IsPositiveInfinity(p))
                return Norm.Lukasiewicz.Function(x, y);
            return Log(1 + ((Pow(p, x) - 1) * (Pow(p, y) - 1)) / (p - 1), p);
        }, (int) PUnitorType.Frank);

    public static readonly PUnitor AczelAlsina =
        new(nameof(AczelAlsina), "Aczél-Alsina", (x, y, p) =>
        {
            // p = 0
            if (Abs(p) <= Epsilon)
                return Norm.Drastic.Function(x, y);
            // 0 <= p < +∞
            if (p is > 0 and < double.PositiveInfinity)
                return Exp(-Pow(Pow(Abs(-Log(x)), p) + Pow(Abs(-Log(y)), p), 1 / p));
            // p = +∞
            return Norm.Minimum.Function(x, y);
        }, (int) PUnitorType.AczelAlsina);

    public static readonly PUnitor Dombi =
        new(nameof(Dombi), "Dombi", (x, y, p) =>
        {
            // x = 0 ∨ y = 0
            if (Abs(x) <= Epsilon || Abs(y) <= Epsilon)
                return 0;
            // p = 0
            if (Abs(p) <= Epsilon)
                return Norm.Drastic.Function(x, y);
            // p = +∞
            if (double.IsPositiveInfinity(p))
                return Norm.Minimum.Function(x, y);
            return 1 / (1 + Pow(Pow((1 - x) / x, p) + Pow((1 - y) / y, p), 1 / p));
        }, (int) PUnitorType.Dombi);

    private PUnitor(string name, string readableName, Func<FuzzyNumber, FuzzyNumber, double, FuzzyNumber> function, int value) : base(name, value)
    {
        ReadableName = readableName;
        Function = function;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, FuzzyNumber, double, FuzzyNumber> Function { get; }
}
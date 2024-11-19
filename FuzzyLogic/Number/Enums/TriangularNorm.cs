using Ardalis.SmartEnum;
using FuzzyLogic.Enum;
using static System.Math;

namespace FuzzyLogic.Number.Enums;

public class TriangularNorm : SmartEnum<TriangularNorm>, IEnum<TriangularNorm, NormToken>
{
    public static readonly TriangularNorm Minimum =
        new(nameof(Minimum), "Minimum", (x, y) => Min(x.Value, y.Value), (int) NormToken.Minimum);

    public static readonly TriangularNorm Product =
        new(nameof(Product), "Product", (x, y) => x.Value * y.Value, (int) NormToken.Product);

    public static readonly TriangularNorm Lukasiewicz =
        new(nameof(Lukasiewicz), "Lukasiewicz", (x, y) => Max(0, x.Value + y.Value - 1), (int) NormToken.Lukasiewicz);

    public static readonly TriangularNorm NilpotentMinimum =
        new(nameof(NilpotentMinimum), "Nilpotent Minimum", (x, y) => x.Value + y.Value > 1 ? Min(x.Value, y.Value) : 0,
            (int) NormToken.NilpotentMinimum);

    private TriangularNorm(string name, string readableName, Func<FuzzyNumber, FuzzyNumber, FuzzyNumber> disjunction, int value) : base(name, value)
    {
        ReadableName = readableName;
        Disjunction = disjunction;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, FuzzyNumber, FuzzyNumber> Disjunction { get; }
}

public enum NormToken
{
    Minimum = 1,
    Product = 2,
    Lukasiewicz = 3,
    NilpotentMinimum = 4
}
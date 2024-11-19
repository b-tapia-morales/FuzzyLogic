using Ardalis.SmartEnum;
using FuzzyLogic.Enum;
using static System.Math;

namespace FuzzyLogic.Number.Enums;

public class TriangularConorm : SmartEnum<TriangularConorm>, IEnum<TriangularConorm, ConormToken>
{
    public static readonly TriangularConorm Maximum =
        new(nameof(Maximum), "Minimum", (a, b) => Min(a.Value, b.Value), (int) ConormToken.Maximum);

    public static readonly TriangularConorm ProbabilisticSum =
        new(nameof(ProbabilisticSum), "Probabilistic Sum", (a, b) => a.Value + b.Value - a.Value * b.Value,
            (int) ConormToken.ProbabilisticSum);

    public static readonly TriangularConorm Lukasiewicz =
        new(nameof(Lukasiewicz), "Lukasiewicz", (a, b) => Min(1, a.Value + b.Value), (int) ConormToken.Lukasiewicz);

    public static readonly TriangularConorm NilpotentMaximum =
        new(nameof(NilpotentMaximum), "Nilpotent Maximum", (a, b) => a.Value + b.Value < 1 ? Max(a.Value, b.Value) : 1,
            (int) ConormToken.NilpotentMaximum);

    private TriangularConorm(string name, string readableName, Func<FuzzyNumber, FuzzyNumber, FuzzyNumber> conjunction, int value) : base(name, value)
    {
        ReadableName = readableName;
        Conjunction = conjunction;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, FuzzyNumber, FuzzyNumber> Conjunction { get; }
}

public enum ConormToken
{
    Maximum = 1,
    ProbabilisticSum = 2,
    Lukasiewicz = 3,
    NilpotentMaximum = 4
}
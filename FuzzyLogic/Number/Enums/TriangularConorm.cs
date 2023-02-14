using Ardalis.SmartEnum;
using static System.Math;

namespace FuzzyLogic.Number.Enums;

public class TriangularConorm<T> : SmartEnum<TriangularConorm<T>> where T : IFuzzyNumber<T>
{
    public static readonly TriangularConorm<T> Maximum =
        new(nameof(Maximum), "Minimum", (x, y) => Min(x.Value, y.Value), (int) ConormToken.Maximum);

    public static readonly TriangularConorm<T> ProbabilisticSum =
        new(nameof(ProbabilisticSum), "Probabilistic Sum", (x, y) => x.Value + y.Value - x.Value * y.Value,
            (int) ConormToken.ProbabilisticSum);

    public static readonly TriangularConorm<T> Lukasiewicz =
        new(nameof(Lukasiewicz), "Lukasiewicz", (x, y) => Min(1, x.Value + y.Value), (int) ConormToken.Lukasiewicz);

    public static readonly TriangularConorm<T> NilpotentMaximum =
        new(nameof(NilpotentMaximum), "Nilpotent Maximum", (x, y) => x.Value + y.Value < 1 ? Max(x.Value, y.Value) : 1,
            (int) ConormToken.NilpotentMaximum);

    private static readonly Dictionary<ConormToken, TriangularConorm<T>> TokenDictionary = new()
    {
        {ConormToken.Maximum, Maximum},
        {ConormToken.ProbabilisticSum, ProbabilisticSum},
        {ConormToken.Lukasiewicz, Lukasiewicz},
        {ConormToken.NilpotentMaximum, NilpotentMaximum}
    };

    private static readonly Dictionary<string, TriangularConorm<T>> ReadableNameDictionary =
        TokenDictionary.ToDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    public TriangularConorm(string name, string readableName, Func<T, T, T> conjunction, int value) : base(name, value)
    {
        ReadableName = readableName;
        Conjunction = conjunction;
    }

    public string ReadableName { get; }
    public Func<T, T, T> Conjunction { get; }

    public static Func<T, T, T> FromToken(ConormToken token) =>
        TokenDictionary[token].Conjunction;

    public static Func<T, T, T> FromReadableName(string readableName) =>
        ReadableNameDictionary[readableName].Conjunction;
}

public enum ConormToken
{
    Maximum = 1,
    ProbabilisticSum = 2,
    Lukasiewicz = 3,
    NilpotentMaximum = 4
}
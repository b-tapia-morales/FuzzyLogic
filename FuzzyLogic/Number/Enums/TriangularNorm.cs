using Ardalis.SmartEnum;
using static System.Math;

namespace FuzzyLogic.Number.Enums;

public class TriangularNorm<T> : SmartEnum<TriangularNorm<T>> where T : IFuzzyNumber<T>
{
    public static readonly TriangularNorm<T> Minimum =
        new(nameof(Minimum), "Minimum", (x, y) => Min(x.Value, y.Value), (int) NormToken.Minimum);

    public static readonly TriangularNorm<T> Product =
        new(nameof(Product), "Product", (x, y) => x.Value * y.Value, (int) NormToken.Product);

    public static readonly TriangularNorm<T> Lukasiewicz =
        new(nameof(Lukasiewicz), "Lukasiewicz", (x, y) => Max(0, x.Value + y.Value - 1), (int) NormToken.Lukasiewicz);

    public static readonly TriangularNorm<T> NilpotentMinimum =
        new(nameof(NilpotentMinimum), "Nilpotent Minimum", (x, y) => x.Value + y.Value > 1 ? Min(x.Value, y.Value) : 0,
            (int) NormToken.NilpotentMinimum);

    private static readonly Dictionary<NormToken, TriangularNorm<T>> TokenDictionary = new()
    {
        {NormToken.Minimum, Minimum},
        {NormToken.Product, Product},
        {NormToken.Lukasiewicz, Lukasiewicz},
        {NormToken.NilpotentMinimum, NilpotentMinimum}
    };

    private static readonly Dictionary<string, TriangularNorm<T>> ReadableNameDictionary =
        TokenDictionary.ToDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    public TriangularNorm(string name, string readableName, Func<T, T, T> disjunction, int value) : base(name, value)
    {
        ReadableName = readableName;
        Disjunction = disjunction;
    }

    public string ReadableName { get; }
    public Func<T, T, T> Disjunction { get; }

    public static Func<T, T, T> FromToken(NormToken token) =>
        TokenDictionary[token].Disjunction;

    public static Func<T, T, T> FromReadableName(string readableName) =>
        ReadableNameDictionary[readableName].Disjunction;
}

public enum NormToken
{
    Minimum = 1,
    Product = 2,
    Lukasiewicz = 3,
    NilpotentMinimum = 4
}
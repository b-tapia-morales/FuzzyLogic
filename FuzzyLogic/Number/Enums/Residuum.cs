using Ardalis.SmartEnum;
using static System.Math;

namespace FuzzyLogic.Number.Enums;

public class Residuum<T> : SmartEnum<Residuum<T>> where T : IFuzzyNumber<T>
{
    public static readonly Residuum<T> Godel =
        new(nameof(Godel), "Gödel", (x, y) => x.Value <= y.Value ? 1 : y.Value, (int) ResiduumToken.Godel);

    public static readonly Residuum<T> Goguen =
        new(nameof(Goguen), "Goguen", (x, y) => x.Value <= y.Value ? 1 : y.Value / x.Value, (int) ResiduumToken.Goguen);

    public static readonly Residuum<T> Lukasiewicz =
        new(nameof(Lukasiewicz), "Lukasiewicz", (x, y) => Min(1, 1 - x.Value + y.Value),
            (int) ResiduumToken.Lukasiewicz);

    public static readonly Residuum<T> KleeneDienes =
        new(nameof(KleeneDienes), "Kleene-Dienes", (x, y) => Max(1 - x.Value, y.Value),
            (int) ResiduumToken.KleeneDienes);

    private static readonly Dictionary<ResiduumToken, Residuum<T>> TokenDictionary = new()
    {
        {ResiduumToken.Godel, Godel},
        {ResiduumToken.Goguen, Goguen},
        {ResiduumToken.Lukasiewicz, Lukasiewicz},
        {ResiduumToken.KleeneDienes, KleeneDienes}
    };

    private static readonly Dictionary<string, Residuum<T>> ReadableNameDictionary =
        TokenDictionary.ToDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    public Residuum(string name, string readableName, Func<T, T, T> implication, int value) : base(name, value)
    {
        ReadableName = readableName;
        Implication = implication;
    }

    public string ReadableName { get; }
    public Func<T, T, T> Implication { get; }

    public static Func<T, T, T> FromToken(ResiduumToken token) => 
        TokenDictionary[token].Implication;

    public static Func<T, T, T> FromReadableName(string readableName) =>
        ReadableNameDictionary[readableName].Implication;
}

public enum ResiduumToken
{
    Godel = 1,
    Goguen = 2,
    Lukasiewicz = 3,
    KleeneDienes = 4
}
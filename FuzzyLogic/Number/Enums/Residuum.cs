using Ardalis.SmartEnum;
using FuzzyLogic.Enum;
using static System.Math;

namespace FuzzyLogic.Number.Enums;

public class Residuum : SmartEnum<Residuum>, IEnum<Residuum, ResiduumToken>
{
    public static readonly Residuum Godel =
        new(nameof(Godel), "Gödel", (x, y) => x.Value <= y.Value ? 1 : y.Value, (int) ResiduumToken.Godel);

    public static readonly Residuum Goguen =
        new(nameof(Goguen), "Goguen", (x, y) => x.Value <= y.Value ? 1 : y.Value / x.Value, (int) ResiduumToken.Goguen);

    public static readonly Residuum Lukasiewicz =
        new(nameof(Lukasiewicz), "Lukasiewicz", (x, y) => Min(1, 1 - x.Value + y.Value),
            (int) ResiduumToken.Lukasiewicz);

    public static readonly Residuum KleeneDienes =
        new(nameof(KleeneDienes), "Kleene-Dienes", (x, y) => Max(1 - x.Value, y.Value),
            (int) ResiduumToken.KleeneDienes);

    private Residuum(string name, string readableName, Func<FuzzyNumber, FuzzyNumber, FuzzyNumber> implication, int value) : base(name, value)
    {
        ReadableName = readableName;
        Implication = implication;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, FuzzyNumber, FuzzyNumber> Implication { get; }
}

public enum ResiduumToken
{
    Godel = 1,
    Goguen = 2,
    Lukasiewicz = 3,
    KleeneDienes = 4
}
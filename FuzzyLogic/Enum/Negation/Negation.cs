using Ardalis.SmartEnum;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Enum.Negation;

public class Negation : SmartEnum<Negation>, IEnum<Negation, NegationType>, INegation
{
    public static readonly Negation Standard =
        new(nameof(Standard), "Standard", a => 1 - a.Value, (int) NegationType.Standard);

    public static readonly Negation RaisedCosine =
        new(nameof(RaisedCosine), "Raised Cosine", a => (1 / 2.0) * (1 + Cos(PI * a.Value)),
            (int) NegationType.RaisedCosine);

    private Negation(string name, string readableName, Func<FuzzyNumber, FuzzyNumber> function, int value) : base(name, value)
    {
        ReadableName = readableName;
        Function = function;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, FuzzyNumber> Function { get; }

    public FuzzyNumber Complement(FuzzyNumber x) => Function(x);
}

public enum NegationType
{
    Standard,
    RaisedCosine
}
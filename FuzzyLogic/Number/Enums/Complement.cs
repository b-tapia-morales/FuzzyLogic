using Ardalis.SmartEnum;
using FuzzyLogic.Enum;
using static System.Math;

namespace FuzzyLogic.Number.Enums;

public class Complement : SmartEnum<Complement>, IEnum<Complement, ComplementToken>
{
    public static readonly Complement Standard =
        new(nameof(Standard), "Standard", x => 1 - x.Value, (int) ComplementToken.Standard);

    public static readonly Complement RaisedCosine =
        new(nameof(RaisedCosine), "Raised Cosine", x => (1 / 2.0) * (1 + Cos(PI * x.Value)),
            (int) ComplementToken.RaisedCosine);

    private Complement(string name, string readableName, Func<FuzzyNumber, FuzzyNumber> negation, int value) : base(name, value)
    {
        ReadableName = readableName;
        Negation = negation;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, FuzzyNumber> Negation { get; }
}

public enum ComplementToken
{
    Standard = 1,
    RaisedCosine = 2
}
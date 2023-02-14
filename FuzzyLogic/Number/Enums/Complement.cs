using Ardalis.SmartEnum;
using static System.Math;

namespace FuzzyLogic.Number.Enums;

public class Complement<T> : SmartEnum<Complement<T>> where T : IFuzzyNumber<T>
{
    public static readonly Complement<T> Standard =
        new(nameof(Standard), "Standard", x => 1 - x.Value, (int) ComplementToken.Standard);

    public static readonly Complement<T> RaisedCosine =
        new(nameof(RaisedCosine), "Raised Cosine", x => (1 / 2.0) * (1 + Cos(PI * x.Value)),
            (int) ComplementToken.RaisedCosine);

    private static readonly Dictionary<ComplementToken, Complement<T>> TokenDictionary = new()
    {
        {ComplementToken.Standard, Standard},
        {ComplementToken.RaisedCosine, RaisedCosine},
    };

    private static readonly Dictionary<string, Complement<T>> ReadableNameDictionary =
        TokenDictionary.ToDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    public Complement(string name, string readableName, Func<T, T> negation, int value) : base(name, value)
    {
        ReadableName = readableName;
        Negation = negation;
    }

    public string ReadableName { get; }
    public Func<T, T> Negation { get; }

    public static Func<T, T> FromToken(ComplementToken token) =>
        TokenDictionary[token].Negation;

    public static Func<T, T> FromReadableName(string readableName) =>
        ReadableNameDictionary[readableName].Negation;
}

public enum ComplementToken
{
    Standard = 1,
    RaisedCosine = 2
}
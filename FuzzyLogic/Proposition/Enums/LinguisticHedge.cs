using Ardalis.SmartEnum;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Proposition.Enums;

public class LinguisticHedge<T> : SmartEnum<LinguisticHedge<T>> where T : IFuzzyNumber<T>
{
    public static readonly LinguisticHedge<T> None =
        new(nameof(None), string.Empty, y => y, (int) HedgeToken.None);

    public static readonly LinguisticHedge<T> Very =
        new(nameof(Very), "Very", y => T.Of(Pow(y.Value, 2)), (int) HedgeToken.Very);

    public static readonly LinguisticHedge<T> VeryVery =
        new(nameof(VeryVery), "Very, very", y => T.Of(Pow(y.Value, 4)), (int) HedgeToken.VeryVery);

    public static readonly LinguisticHedge<T> Plus =
        new(nameof(Plus), "Plus", y => T.Of(Pow(y.Value, 5 / 4.0)), (int) HedgeToken.Plus);

    public static readonly LinguisticHedge<T> Slightly =
        new(nameof(Slightly), "Slightly", y => T.Of(Sqrt(y.Value)), (int) HedgeToken.Slightly);

    public static readonly LinguisticHedge<T> Minus =
        new(nameof(Minus), "Minus", y => T.Of(Pow(y.Value, 3 / 4.0)), (int) HedgeToken.Minus);

    public static readonly LinguisticHedge<T> Indeed = new(nameof(Indeed), "Indeed",
        y =>
        {
            if (Abs(y.Value - 0.5) < IFuzzyNumber<T>.Tolerance) return y;
            return y.Value < 0.5 ? T.Of(2 * Pow(y.Value, 2)) : T.Of(1 - 2 * Pow(1 - y.Value, 2));
        }, (int) HedgeToken.Indeed);

    private static readonly Dictionary<HedgeToken, LinguisticHedge<T>> TokenDictionary = new()
    {
        {HedgeToken.None, None},
        {HedgeToken.Very, Very},
        {HedgeToken.VeryVery, VeryVery},
        {HedgeToken.Plus, Plus},
        {HedgeToken.Slightly, Slightly},
        {HedgeToken.Minus, Minus},
        {HedgeToken.Indeed, Indeed}
    };

    private static readonly Dictionary<string, LinguisticHedge<T>> ReadableNameDictionary =
        TokenDictionary.ToDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    public LinguisticHedge(string name, string readableName, Func<T, T> function, int value) :
        base(name, value)
    {
        ReadableName = readableName;
        Function = function;
    }

    public string ReadableName { get; }
    public Func<T, T> Function { get; }

    public static LinguisticHedge<T> FromToken(HedgeToken token) => TokenDictionary[token];

    public static LinguisticHedge<T> FromReadableName(string readableName) => ReadableNameDictionary[readableName];

    public override string ToString() => ReadableName;
}

public enum HedgeToken
{
    None = 0,
    Very = 1,
    VeryVery = 2,
    Plus = 3,
    Slightly = 4,
    Minus = 5,
    Indeed = 6
}
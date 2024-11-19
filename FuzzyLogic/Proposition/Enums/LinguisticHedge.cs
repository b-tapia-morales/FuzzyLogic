using Ardalis.SmartEnum;
using FuzzyLogic.Enum;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Proposition.Enums;

public class LinguisticHedge : SmartEnum<LinguisticHedge>, IEnum<LinguisticHedge, HedgeToken>
{
    public static readonly LinguisticHedge None =
        new(nameof(None), string.Empty, y => y, (int) HedgeToken.None);

    public static readonly LinguisticHedge Very =
        new(nameof(Very), "Very", y => Pow(y.Value, 2), (int) HedgeToken.Very);

    public static readonly LinguisticHedge VeryVery =
        new(nameof(VeryVery), "Very, very", y => Pow(y.Value, 4), (int) HedgeToken.VeryVery);

    public static readonly LinguisticHedge Plus =
        new(nameof(Plus), "Plus", y => Pow(y.Value, 5 / 4.0), (int) HedgeToken.Plus);

    public static readonly LinguisticHedge Slightly =
        new(nameof(Slightly), "Slightly", y => Sqrt(y.Value), (int) HedgeToken.Slightly);

    public static readonly LinguisticHedge Minus =
        new(nameof(Minus), "Minus", y => Pow(y.Value, 3 / 4.0), (int) HedgeToken.Minus);

    public static readonly LinguisticHedge Indeed = new(nameof(Indeed), "Indeed",
        y =>
        {
            if (y == 0.5) return y;
            return y < 0.5 ? 2 * Pow(y.Value, 2) : 1 - 2 * Pow(1 - y.Value, 2);
        }, (int) HedgeToken.Indeed);

    private LinguisticHedge(string name, string readableName, Func<FuzzyNumber, FuzzyNumber> function, int value) :
        base(name, value)
    {
        ReadableName = readableName;
        Function = function;
    }

    public string ReadableName { get; }
    public Func<FuzzyNumber, FuzzyNumber> Function { get; }
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
using Ardalis.SmartEnum;

namespace FuzzyLogic.Condition;

public class LinguisticHedge : SmartEnum<LinguisticHedge>
{
    public static readonly LinguisticHedge None = new(nameof(None), string.Empty, HedgeToken.None,
        y => y, (int) HedgeToken.None);
    
    public static readonly LinguisticHedge Very = new(nameof(Very), "Very", HedgeToken.Very,
        y => Math.Pow(y.Value, 2), (int) HedgeToken.Very);

    public static readonly LinguisticHedge VeryVery = new(nameof(VeryVery), "Very, very", HedgeToken.VeryVery,
        y => Math.Pow(y.Value, 4), (int) HedgeToken.VeryVery);

    public static readonly LinguisticHedge Plus = new(nameof(Plus), "Plus", HedgeToken.Plus,
        y => Math.Pow(y.Value, 1.25), (int) HedgeToken.Plus);

    public static readonly LinguisticHedge Slightly = new(nameof(Slightly), "Slightly", HedgeToken.Slightly,
        y => Math.Sqrt(y.Value), (int) HedgeToken.Slightly);

    public static readonly LinguisticHedge Minus = new(nameof(Minus), "Minus", HedgeToken.Minus,
        y => Math.Pow(y.Value, 0.75), (int) HedgeToken.Minus);

    public static readonly LinguisticHedge Indeed = new(nameof(Indeed), "Indeed", HedgeToken.Indeed,
        y =>
        {
            if (y == 0.5) return y;
            return y < 0.5 ? 2 * Math.Pow(y.Value, 2) : 1 - 2 * Math.Pow(1 - y.Value, 2);
        }, (int) HedgeToken.Indeed);

    private static readonly Dictionary<HedgeToken, LinguisticHedge> TokenDictionary = new()
    {
        {HedgeToken.None, None},
        {HedgeToken.Very, Very},
        {HedgeToken.VeryVery, VeryVery},
        {HedgeToken.Plus, Plus},
        {HedgeToken.Slightly, Slightly},
        {HedgeToken.Minus, Minus},
        {HedgeToken.Indeed, Indeed}
    };

    private static readonly Dictionary<string, LinguisticHedge> ReadableNameDictionary =
        TokenDictionary.ToDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    public LinguisticHedge(string name, string readableName, HedgeToken token, Func<FuzzyNumber, FuzzyNumber> function,
        int value) : base(name, value)
    {
        ReadableName = readableName;
        Token = token;
        Function = function;
    }

    public string ReadableName { get; }
    public HedgeToken Token { get; }
    public Func<FuzzyNumber, FuzzyNumber> Function { get; }

    public static LinguisticHedge FromToken(HedgeToken token) => TokenDictionary[token];

    public static LinguisticHedge FromReadableName(string readableName) => ReadableNameDictionary[readableName];

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
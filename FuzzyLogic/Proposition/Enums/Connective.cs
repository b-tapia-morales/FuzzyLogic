using Ardalis.SmartEnum;
using FuzzyLogic.Number;

namespace FuzzyLogic.Proposition.Enums;

public class Connective<T> : SmartEnum<Connective<T>> where T: IFuzzyNumber<T>
{
    public static readonly Connective<T> None =
        new(nameof(None), string.Empty, null, (int) ConnectiveToken.None);

    public static readonly Connective<T> If =
        new(nameof(If), "IF", null, (int) ConnectiveToken.Antecedent);

    public static readonly Connective<T> And =
        new(nameof(And), "AND", (a, b) => a & b, (int) ConnectiveToken.Conjunction);

    public static readonly Connective<T> Or =
        new(nameof(Or), "OR", (a, b) => a | b, (int) ConnectiveToken.Disjunction);
    
    public static readonly Connective<T> Then =
        new(nameof(Then), "THEN", T.Implication, (int) ConnectiveToken.Consequent);

    private static readonly Dictionary<ConnectiveToken, Connective<T>> TokenDictionary = new()
    {
        {ConnectiveToken.None, None},
        {ConnectiveToken.Antecedent, If},
        {ConnectiveToken.Consequent, Then},
        {ConnectiveToken.Conjunction, And},
        {ConnectiveToken.Disjunction, Or}
    };

    private static readonly Dictionary<string, Connective<T>> ReadableNameDictionary =
        TokenDictionary.ToDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    public Connective(string name, string readableName, Func<T, T, T>? function,
        int value) : base(name, value)
    {
        ReadableName = readableName;
        Function = function;
    }

    public string ReadableName { get; }
    public Func<T, T, T>? Function { get; }

    public static Connective<T> FromToken(ConnectiveToken token) => TokenDictionary[token];

    public static Connective<T> FromReadableName(string readableName) => ReadableNameDictionary[readableName];

    public override string ToString() => ReadableName;
}

public enum ConnectiveToken
{
    None = 0,
    Antecedent = 1,
    Consequent = 2,
    Conjunction = 3,
    Disjunction = 4
}
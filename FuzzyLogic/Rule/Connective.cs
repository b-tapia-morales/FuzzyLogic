using Ardalis.SmartEnum;
using static FuzzyLogic.Rule.ConnectiveToken;

namespace FuzzyLogic.Rule;

public class Connective : SmartEnum<Connective>
{
    public static readonly Connective If = new(nameof(If), "IF", Antecedent, (int) Antecedent);
    public static readonly Connective Then = new(nameof(Then), "THEN", Consequent, (int) Antecedent);
    public static readonly Connective And = new(nameof(And), "AND", Conjunction, (int) Conjunction);
    public static readonly Connective Or = new(nameof(Or), "OR", Disjunction, (int) Disjunction);

    private static readonly Dictionary<ConnectiveToken, Connective> Dictionary = new()
    {
        {Antecedent, If},
        {Consequent, Then},
        {Conjunction, And},
        {Disjunction, Or},
    };

    public static readonly IReadOnlyDictionary<ConnectiveToken, Connective> ReadOnlyDictionary = Dictionary;

    public Connective(string name, string readableName, ConnectiveToken token, int value) : base(name, value)
    {
        ReadableName = readableName;
        Token = token;
    }

    public string ReadableName { get; }
    public ConnectiveToken Token { get; }
}
using Ardalis.SmartEnum;
using static FuzzyLogic.Condition.LiteralToken;

namespace FuzzyLogic.Condition;

public class Literal : SmartEnum<Literal>
{
    public static readonly Literal Is = new(nameof(Is), "IS", Affirmation, (int) Affirmation);
    public static readonly Literal IsNot = new(nameof(IsNot), "IS NOT", Negation, (int) Negation);

    private static readonly Dictionary<LiteralToken, Literal> Dictionary = new()
    {
        {Affirmation, Is},
        {Negation, IsNot}
    };

    public static readonly IReadOnlyDictionary<LiteralToken, Literal> ReadOnlyDictionary = Dictionary;

    public Literal(string name, string readableName, LiteralToken token, int value) : base(name, value)
    {
        ReadableName = readableName;
        Token = token;
    }
    
    public string ReadableName { get; }
    public LiteralToken Token { get; }
    
    public override string ToString() => ReadableName;
}
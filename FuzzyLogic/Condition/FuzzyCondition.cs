using FuzzyLogic.Linguistics;
using FuzzyLogic.MembershipFunctions;

namespace FuzzyLogic.Condition;

public class FuzzyCondition<T> where T : unmanaged, IConvertible
{
    public Literal Literal { get; }
    public LinguisticVariable<T> LinguisticVariable { get; }
    public IMembershipFunction<T> Function { get; }

    public FuzzyCondition(LiteralToken literal, LinguisticVariable<T> linguisticVariable,
        IMembershipFunction<T> function)
    {
        Literal = Literal.ReadOnlyDictionary[literal];
        LinguisticVariable = linguisticVariable;
        Function = function;
    }

    public override string ToString() => $"{LinguisticVariable} {Literal.ReadableName} {Function.Name}";
}
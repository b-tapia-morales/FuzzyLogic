using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition.Enums;

namespace FuzzyLogic.Proposition;

public sealed class FuzzyProposition(string variableName, Connective connective, Literal literal, LinguisticHedge linguisticHedge, IMembershipFunction function) : IProposition
{
    public string VariableName { get; } = variableName;
    public Connective Connective { get; } = connective;
    public Literal Literal { get; } = literal;
    public LinguisticHedge LinguisticHedge { get; } = linguisticHedge;
    public IMembershipFunction Function { get; } = function;

    public override string ToString()
    {
        var hedge = LinguisticHedge != LinguisticHedge.None ? $"{LinguisticHedge.ReadableName} " : string.Empty;
        return $"{Connective} {VariableName} {Literal.ReadableName} {hedge}{Function.Name}";
    }

    public bool Equals(IProposition? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return string.Equals(VariableName, other.VariableName, StringComparison.InvariantCultureIgnoreCase) &&
               Literal == other.Literal && LinguisticHedge == other.LinguisticHedge &&
               string.Equals(Function.Name, other.Function.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        return obj.GetType() == GetType() && Equals((FuzzyProposition) obj);
    }

    public override int GetHashCode() => HashCode.Combine(VariableName, Literal.Name, LinguisticHedge.Name, Function.Name);

    public bool Equals(IProposition? x, IProposition? y)
    {
        if (x is null && y is null)
            return true;
        if (x is null || y is null)
            return false;
        return x.Equals(y);
    }

    public int GetHashCode(IProposition obj) => obj.GetHashCode();

    public bool IsApplicable(IDictionary<string, double> facts) =>
        facts.ContainsKey(Function.Name);

    public FuzzyNumber ApplyUnaryOperators(double crispNumber, INegation negation)
    {
        var membershipFunction = Function.PureFunction();
        var hedgeFunction = LinguisticHedge.Function;
        var fuzzyNumber = hedgeFunction(membershipFunction(crispNumber));
        return Literal == Literal.IsNot ? negation.Complement(fuzzyNumber) : fuzzyNumber;
    }
}
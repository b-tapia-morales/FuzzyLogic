using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition.Enums;

namespace FuzzyLogic.Proposition;

public interface IProposition: IEquatable<IProposition>, IEqualityComparer<IProposition>
{
    string VariableName { get; }
    Connective Connective { get; }
    Literal Literal { get; }
    LinguisticHedge LinguisticHedge { get; }
    IMembershipFunction Function { get; }

    bool IsApplicable(IDictionary<string, double> facts);

    FuzzyNumber ApplyUnaryOperators(double crispNumber, INegation negation);

    FuzzyNumber ApplyUnaryOperators(double crispNumber) => ApplyUnaryOperators(crispNumber, Negation.Standard);
}
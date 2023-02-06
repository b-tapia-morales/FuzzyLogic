using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition.Enums;
using FuzzyLogic.Variable;

namespace FuzzyLogic.Proposition;

public interface IProposition<T> where T : struct, IFuzzyNumber<T>
{
    Connective<T> Connective { get; set; }
    IVariable LinguisticVariable { get; }
    Literal<T> Literal { get; }
    LinguisticHedge<T> LinguisticHedge { get; }
    IMembershipFunction<double> Function { get; }

    bool IsApplicable(IDictionary<string, double> facts);

    T ApplyUnaryOperators(double crispNumber);
}
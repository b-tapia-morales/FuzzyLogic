using FuzzyLogic.Function.Interface;
using FuzzyLogic.Function.Real;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition.Enums;
using FuzzyLogic.Variable;

namespace FuzzyLogic.Proposition;

public interface IProposition
{
    Connective Connective { get; set; }
    IVariable LinguisticVariable { get; }
    Literal Literal { get; }
    LinguisticHedge LinguisticHedge { get; }
    IRealFunction Function { get; }

    FuzzyNumber ApplyUnaryOperators(double crispNumber);
}
using FuzzyLogic.Knowledge;
using FuzzyLogic.Linguistics;
using FuzzyLogic.MembershipFunctions.Real;
using FuzzyLogic.Proposition.Enums;

namespace FuzzyLogic.Proposition;

public interface IProposition
{
    Connective Connective { get; set; }
    IVariable LinguisticVariable { get; }
    Literal Literal { get; }
    LinguisticHedge LinguisticHedge { get; }
    IRealFunction Function { get; }
}
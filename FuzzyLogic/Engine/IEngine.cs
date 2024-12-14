using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Enum.Family;
using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.Residuum;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Memory;

namespace FuzzyLogic.Engine;

public interface IEngine
{
    IRuleBase RuleBase { get; }
    IWorkingMemory WorkingMemory { get; }
    IOperatorFamily OperatorFamily { get; set; }
    ImplicationMethod ImplicationMethod { get; set; }
    IDefuzzifier Defuzzifier { get; set; }

    IEngine UseImplicationMethod(ImplicationMethod method);

    IEngine UseDefuzzificationMethod(DefuzzificationMethod method);

    IEngine UseNegator(INegation negation);

    IEngine UseDisjunction(INorm norm);

    IEngine UseConjunction(IConorm conorm);

    IEngine UseResiduum(IResiduum residuum);

    IEngine UseOperators(INegation negation, INorm norm, IConorm conorm, IResiduum residuum);

    IEngine UseOperatorFamily(IOperatorFamily operatorFamily);

    IEngine UseCanonicalFamily(CanonicalType type);

    IEngine UseParameterizedFamily(ParameterizedType type, double gamma, double alpha, double beta, double omega);

    double? Defuzzify(string variableName, bool provideExplanation = true);
}
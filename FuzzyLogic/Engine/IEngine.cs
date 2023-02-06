using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Memory;
using FuzzyLogic.Number;
using static FuzzyLogic.Engine.Defuzzify.DefuzzificationMethod;

namespace FuzzyLogic.Engine;

public interface IEngine<T> where T : struct, IFuzzyNumber<T>
{
    IKnowledgeBase<T> KnowledgeBase { get; }
    IWorkingMemory WorkingMemory { get; }
    IDefuzzifier<T> Defuzzifier { get; }

    static abstract IEngine<T> Create(IKnowledgeBase<T> knowledgeBase, IWorkingMemory workingMemory,
        DefuzzificationMethod method = MeanOfMaxima);

    double? Defuzzify(string variableName, bool provideExplanation = true);
}
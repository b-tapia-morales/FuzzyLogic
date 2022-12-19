using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Memory;
using static FuzzyLogic.Engine.Defuzzify.DefuzzificationMethod;

namespace FuzzyLogic.Engine;

public interface IEngine
{
    IKnowledgeBase KnowledgeBase { get; set; }
    IWorkingMemory WorkingMemory { get; set; }
    IDefuzzifier Defuzzifier { get; set; }

    static abstract IEngine Create(IKnowledgeBase knowledgeBase, IWorkingMemory workingMemory,
        DefuzzificationMethod method = MeanOfMaxima);

    double? Defuzzify(string variableName, bool provideExplanation = true);
}
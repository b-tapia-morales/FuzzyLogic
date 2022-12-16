using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Memory;

namespace FuzzyLogic.Engine;

public interface IEngine
{
    IKnowledgeBase KnowledgeBase { get; set; }
    IWorkingMemory WorkingMemory { get; set; }

    //IEngine ExcludeRulesWithKnownFacts();

    //IEnumerable<string> ApplicableFromAvailableFacts();

    static abstract IEngine Create(IKnowledgeBase knowledgeBase, IWorkingMemory workingMemory,
        DefuzzificationMethod method = DefuzzificationMethod.MeanOfMaxima);

    double? Defuzzify(string variableName);
}
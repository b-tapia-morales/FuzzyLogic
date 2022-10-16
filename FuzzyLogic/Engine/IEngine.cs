using FuzzyLogic.Knowledge;
using FuzzyLogic.Memory;

namespace FuzzyLogic.Engine;

public interface IEngine
{
    IKnowledgeBase KnowledgeBase { get; }
    IWorkingMemory WorkingMemory { get; }
}
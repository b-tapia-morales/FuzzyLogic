using FuzzyLogic.Knowledge;
using FuzzyLogic.Memory;

namespace FuzzyLogic.Engine;

public class InferenceEngine : IEngine
{
    public IKnowledgeBase KnowledgeBase { get; set; }
    public IWorkingMemory WorkingMemory { get; set; }

    private InferenceEngine(IKnowledgeBase @base, IWorkingMemory memory)
    {
        KnowledgeBase = @base;
        WorkingMemory = memory;
    }

    public IEngine ExcludeRulesWithKnownFacts() => ExcludeRulesWithKnownFacts(this);

    public IEnumerable<string> ApplicableFromAvailableFacts() =>
        KnowledgeBase.RuleBase.ProductionRules.Where(e => e.IsApplicable(WorkingMemory.Facts))
            .Select(e => e.Consequent!.LinguisticVariable.Name);

    public static IEngine Create(IKnowledgeBase @base, IWorkingMemory memory) =>
        new InferenceEngine(@base, memory);

    private static IEngine ExcludeRulesWithKnownFacts(IEngine engine)
    {
        engine.KnowledgeBase.RuleBase.ProductionRules = engine.KnowledgeBase.RuleBase.ProductionRules.Where(e =>
            engine.WorkingMemory.Facts.ContainsKey(e.Consequent!.LinguisticVariable.Name)).ToList();
        return engine;
    }
}
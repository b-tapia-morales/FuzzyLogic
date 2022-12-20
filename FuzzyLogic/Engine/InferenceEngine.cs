using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Engine.Defuzzify.Methods;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Memory;
using FuzzyLogic.Tree;

namespace FuzzyLogic.Engine;

public class InferenceEngine : IEngine
{
    public IKnowledgeBase KnowledgeBase { get; set; }
    public IWorkingMemory WorkingMemory { get; set; }
    public IDefuzzifier Defuzzifier { get; set; }

    private InferenceEngine(IKnowledgeBase knowledgeBase, IWorkingMemory workingMemory,
        DefuzzificationMethod method = DefuzzificationMethod.MeanOfMaxima)
    {
        KnowledgeBase = knowledgeBase;
        WorkingMemory = workingMemory;
        Defuzzifier = DefuzzifierFactory.CreateInstance(method);
    }

    public static IEngine Create(IKnowledgeBase knowledgeBase, IWorkingMemory workingMemory,
        DefuzzificationMethod method = DefuzzificationMethod.MeanOfMaxima) =>
        new InferenceEngine(knowledgeBase, workingMemory, method);

    public double? Defuzzify(string variableName, bool provideExplanation = true)
    {
        if (WorkingMemory.Facts.TryGetValue(variableName, out var value))
            return value;
        var rootNode = TreeNode.CreateDerivationTree(variableName, KnowledgeBase.RuleBase.ProductionRules,
            KnowledgeBase.RuleBase.RuleComparer, WorkingMemory.Facts);
        var inferredValue = rootNode.InferFact(WorkingMemory.Facts, Defuzzifier);
        if (!provideExplanation)
            return inferredValue;
        rootNode.PrettyWriteTree();
        Console.WriteLine();
        rootNode.WriteTree();
        return inferredValue;
    }
}

public static class DefuzzifierFactory
{
    public static IDefuzzifier CreateInstance(DefuzzificationMethod method) => method switch
    {
        DefuzzificationMethod.FirstOfMaxima => new FirstOfMaxima(),
        DefuzzificationMethod.LastOfMaxima => new LastOfMaxima(),
        DefuzzificationMethod.MeanOfMaxima => new MeanOfMaxima(),
        DefuzzificationMethod.CenterOfSums => new CenterOfSums(),
        DefuzzificationMethod.CenterOfLargestArea => new CenterOfLargestArea(),
        _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
    };
}
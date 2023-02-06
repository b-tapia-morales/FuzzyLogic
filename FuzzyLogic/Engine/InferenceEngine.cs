using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Engine.Defuzzify.Methods;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Memory;
using FuzzyLogic.Number;
using FuzzyLogic.Tree;

namespace FuzzyLogic.Engine;

public class InferenceEngine<T> : IEngine<T> where T : struct, IFuzzyNumber<T>
{
    public IKnowledgeBase<T> KnowledgeBase { get; }
    public IWorkingMemory WorkingMemory { get; }
    public IDefuzzifier<T> Defuzzifier { get; }

    private InferenceEngine(IKnowledgeBase<T> knowledgeBase, IWorkingMemory workingMemory,
        DefuzzificationMethod method = DefuzzificationMethod.MeanOfMaxima)
    {
        KnowledgeBase = knowledgeBase;
        WorkingMemory = workingMemory;
        Defuzzifier = DefuzzifierFactory.CreateInstance<T>(method);
    }

    public static IEngine<T> Create(IKnowledgeBase<T> knowledgeBase, IWorkingMemory workingMemory,
        DefuzzificationMethod method = DefuzzificationMethod.MeanOfMaxima) =>
        new InferenceEngine<T>(knowledgeBase, workingMemory, method);

    public double? Defuzzify(string variableName, bool provideExplanation = true)
    {
        if (WorkingMemory.Facts.TryGetValue(variableName, out var value))
            return value;
        var rootNode = TreeNode<T>.CreateDerivationTree(variableName, KnowledgeBase.RuleBase.ProductionRules,
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
    public static IDefuzzifier<T> CreateInstance<T>(DefuzzificationMethod method)
        where T : struct, IFuzzyNumber<T> => method switch
    {
        DefuzzificationMethod.FirstOfMaxima => new FirstOfMaxima<T>(),
        DefuzzificationMethod.LastOfMaxima => new LastOfMaxima<T>(),
        DefuzzificationMethod.MeanOfMaxima => new MeanOfMaxima<T>(),
        DefuzzificationMethod.CenterOfSums => new CenterOfSums<T>(),
        DefuzzificationMethod.CenterOfLargestArea => new CenterOfLargestArea<T>(),
        _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
    };
}
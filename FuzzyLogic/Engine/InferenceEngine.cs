using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Engine.Defuzzify.Methods;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Memory;
using FuzzyLogic.Number;
using MathNet.Numerics.Integration;
using static FuzzyLogic.Engine.Defuzzify.DefuzzificationMethod;

namespace FuzzyLogic.Engine;

public class InferenceEngine : IEngine
{
    public static readonly IDefuzzifier DefaultDefuzzifier =
        DefuzzifierFactory.CreateInstance(DefuzzificationMethod.MeanOfMaxima);

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

    public static IEngine Create(IKnowledgeBase knowledgeBase, IWorkingMemory memory,
        DefuzzificationMethod method = DefuzzificationMethod.MeanOfMaxima) =>
        new InferenceEngine(knowledgeBase, memory, method);

    public double? Defuzzify(string variableName) =>
        WorkingMemory.Facts.TryGetValue(variableName, out var value) ? value : null;

    public static ICollection<(IRealFunction Function, FuzzyNumber CutPoint)> EvaluateAntecedentWeight(
        IRuleBase ruleBase, IWorkingMemory memory, string variableName) =>
        ruleBase
            .FindRulesWithConclusion(variableName)
            .Select(e => (e.Consequent!.Function, e.EvaluatePremiseWeight(memory.Facts).GetValueOrDefault())).ToList();

    public static ICollection<Func<double, double>> ApplyLambdaCuts(IRuleBase ruleBase, IWorkingMemory memory,
        string variableName)
    {
        if (!memory.Facts.ContainsKey(variableName))
            throw new InvalidOperationException();
        var antecedents = EvaluateAntecedentWeight(ruleBase, memory, variableName);
        return antecedents.Select(e => e.Function.LambdaCutFunction(e.CutPoint)).ToList();
    }

    public static ICollection<double> CalculateArea(IRuleBase ruleBase, IWorkingMemory memory,
        string variableName)
    {
        if (!memory.Facts.ContainsKey(variableName))
            throw new InvalidOperationException();
        var antecedents = EvaluateAntecedentWeight(ruleBase, memory, variableName);
        return antecedents.Select(e =>
            NewtonCotesTrapeziumRule.IntegrateAdaptive(e.Function.LambdaCutFunction(e.CutPoint),
                e.Function.ClosedInterval().X0, e.Function.ClosedInterval().X1, 1e-10)).ToList();
    }

    public static ICollection<(double X0, double X1)> CreateIntervals(ICollection<IRealFunction> functions)
    {
        var intervalEndpoints = new SortedSet<double>();
        foreach (var function in functions)
        {
            var interval = function.ClosedInterval();
            intervalEndpoints.Add(interval.X0);
            intervalEndpoints.Add(interval.X1);
        }

        return intervalEndpoints.Skip(1).Zip(intervalEndpoints, (a, b) => (b, a)).ToList();
    }


    public static IDictionary<(double X0, double X1), ICollection<IRealFunction>> CreateIntervalTable(
        ICollection<IRealFunction> functions)
    {
        var intervalEndpoints = CreateIntervals(functions);
        var intervalTable = new SortedDictionary<(double X0, double X1), ICollection<IRealFunction>>();
        foreach (var interval in intervalEndpoints)
        {
            intervalTable.Add(interval, new List<IRealFunction>());
            foreach (var function in functions)
            {
                var (x0, x1) = function.ClosedInterval();
                if (interval.X0 < x0 || interval.X1 > x1) continue;
                var list = intervalTable[interval];
                list.Add(function);
                intervalTable[interval] = list;
            }
        }

        return intervalTable;
    }
}

public static class DefuzzifierFactory
{
    public static IDefuzzifier CreateInstance(DefuzzificationMethod method) => method switch
    {
        DefuzzificationMethod.FirstOfMaxima => new FirstOfMaxima(),
        DefuzzificationMethod.LastOfMaxima => new LastOfMaxima(),
        DefuzzificationMethod.MeanOfMaxima => new MeanOfMaxima(),
        DefuzzificationMethod.CentreOfGravity => new CentreOfGravity(),
        DefuzzificationMethod.CentreOfSums => new CentreOfSums(),
        DefuzzificationMethod.CentreOfArea => new CentreOfArea(),
        _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
    };
}
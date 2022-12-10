using System.Collections.Immutable;
using FuzzyLogic.Function.Interface;
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

    public static IEngine Create(IKnowledgeBase @base, IWorkingMemory memory) =>
        new InferenceEngine(@base, memory);

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
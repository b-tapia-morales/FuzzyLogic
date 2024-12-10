using FuzzyLogic.Utils.Csv;
using static FuzzyLogic.Memory.EntryResolutionMethod;

// ReSharper disable RedundantExplicitTupleComponentName

namespace FuzzyLogic.Memory;

public class WorkingMemory : IWorkingMemory
{
    private WorkingMemory(EntryResolutionMethod method = Replace)
    {
        Facts = new Dictionary<string, double>();
        Method = method;
    }

    private WorkingMemory(IDictionary<string, double> facts, EntryResolutionMethod method = Replace)
    {
        Facts = facts;
        Method = method;
    }

    public IDictionary<string, double> Facts { get; }
    public EntryResolutionMethod Method { get; }

    public static IWorkingMemory Create(EntryResolutionMethod method = Replace) =>
        new WorkingMemory(method);

    public static IWorkingMemory Create(IDictionary<string, double> facts, EntryResolutionMethod method = Replace) =>
        new WorkingMemory(facts, method);

    public static IWorkingMemory Create(EntryResolutionMethod method, params IEnumerable<(string Key, double Value)> facts) =>
        new WorkingMemory(facts
            .GroupBy(tuple => tuple.Key)
            .Select(group => (Key: group.Key, List: group.Select(e => e.Value)))
            .ToDictionary(tuple => tuple.Key, tuple => method == Replace ? tuple.List.Last() : tuple.List.First()));

    public static IWorkingMemory Create(params IEnumerable<(string Key, double Value)> facts) =>
        Create(Replace, facts);

    public static IWorkingMemory CreateFromFile(string folderPath, bool hasHeader = false, DelimiterType delimiter = DelimiterType.Comma, EntryResolutionMethod method = Replace) =>
        new WorkingMemory(RowRetrieval.RetrieveRows<FactRow, FactMapping>(folderPath, hasHeader, delimiter)
            .GroupBy(tuple => tuple.Key)
            .Select(group => (Key: group.Key, List: group.Select(e => e.Value)))
            .ToDictionary(tuple => tuple.Key, tuple => method == Replace ? tuple.List.Last() : tuple.List.First()));

    public bool ContainsFact(string key) => Facts.ContainsKey(key);

    public double? RetrieveValue(string key) => Facts.TryGetValue(key, out var value) ? value : null;

    public void AddFact(string key, double value)
    {
        if (!Facts.TryAdd(key, value) && Method == Replace)
        {
            Facts[key] = value;
        }
    }

    public void UpdateFact(string key, double value)
    {
        if (!Facts.TryAdd(key, value))
        {
            Facts[key] = value;
        }
    }

    public bool RemoveFact(string key)
    {
        return Facts.Remove(key);
    }

    public override string ToString() => string.Join(Environment.NewLine, Facts.Select(e => $"{e.Key} = {e.Value}"));
}
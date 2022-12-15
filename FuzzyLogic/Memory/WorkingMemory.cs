using FuzzyLogic.Utils.Csv;
using static FuzzyLogic.Memory.EntryResolutionMethod;

namespace FuzzyLogic.Memory;

public class WorkingMemory : IWorkingMemory
{
    protected WorkingMemory(EntryResolutionMethod method = Replace)
    {
        Facts = new Dictionary<string, double>();
        Method = method;
    }

    protected WorkingMemory(IDictionary<string, double> facts, EntryResolutionMethod method = Replace)
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

    public static IWorkingMemory Initialize(EntryResolutionMethod method = Replace) => Create(method);

    public static IWorkingMemory InitializeFromFile(string folderPath, EntryResolutionMethod method = Replace) =>
        new WorkingMemory(
            RowRetrieval.RetrieveRows<FactRow, FactMapping>(folderPath).ToDictionary(e => e.Key, e => e.Value),
            method);

    public IWorkingMemory Clone() => (WorkingMemory) MemberwiseClone();

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
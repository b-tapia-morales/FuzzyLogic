using static FuzzyLogic.Memory.EntryResolutionMethod;

namespace FuzzyLogic.Memory;

public class WorkingMemory : IWorkingMemory<double>
{
    public WorkingMemory(EntryResolutionMethod method = Keep)
    {
        Facts = new Dictionary<string, double>();
        Method = method;
    }

    public WorkingMemory(Dictionary<string, double> dictionary, EntryResolutionMethod method = Keep)
    {
        Facts = dictionary;
        Method = method;
    }

    public Dictionary<string, double> Facts { get; }
    public EntryResolutionMethod Method { get; }

    public bool ContainsFact(string key) => Facts.ContainsKey(key);

    public double? RetrieveValue(string key) => Facts.ContainsKey(key) ? Facts[key] : null;

    public void AddFact(string key, double value)
    {
        if (!Facts.TryAdd(key, value) && Method == Replace)
        {
            Facts[key] = value;
        }
    }

    public override string ToString() => string.Join('\n', Facts);

    public static WorkingMemory InitializeFromFile(string folderName, string fileName,
        EntryResolutionMethod method = Keep) =>
        new(FactRetrieval.RetrieveRows<double>(folderName, fileName).ToDictionary(e => e.Key, e => e.Value), method);
}
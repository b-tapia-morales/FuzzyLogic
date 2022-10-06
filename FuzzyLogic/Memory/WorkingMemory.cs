using FuzzyLogic.Utils.Csv;
using static FuzzyLogic.Memory.EntryResolutionMethod;

namespace FuzzyLogic.Memory;

public class WorkingMemory : IWorkingMemory
{
    private const string CsvFileFolder = "Files";
    private const string CsvFileName = "Facts.csv";

    private static readonly string CsvFilePath =
        Path.Combine(Directory.GetCurrentDirectory(), CsvFileFolder, CsvFileName);

    public WorkingMemory(EntryResolutionMethod method = Preserve)
    {
        Facts = new Dictionary<string, double>();
        Method = method;
    }

    public WorkingMemory(IDictionary<string, double> dictionary, EntryResolutionMethod method = Preserve)
    {
        Facts = dictionary;
        Method = method;
    }

    public IDictionary<string, double> Facts { get; }
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

    public static WorkingMemory InitializeFromFile(string folderPath, EntryResolutionMethod method = Preserve) =>
        new(RowRetrieval.RetrieveRows<FactRow, FactMapping>(folderPath).ToDictionary(e => e.Key, e => e.Value),
            method);

    public static WorkingMemory InitializeFromFile(EntryResolutionMethod method = Preserve) =>
        InitializeFromFile(CsvFilePath, method);
}
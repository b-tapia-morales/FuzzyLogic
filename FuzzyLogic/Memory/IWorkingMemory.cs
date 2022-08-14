namespace FuzzyLogic.Memory;

public interface IWorkingMemory
{
    public Dictionary<string, double> Facts { get; }
    public EntryResolutionMethod Method { get; }

    bool ContainsFact(string key);

    double? RetrieveValue(string key);

    void AddFact(string key, double value);
}
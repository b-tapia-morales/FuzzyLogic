namespace FuzzyLogic.Memory;

public interface IWorkingMemory<T> where T : unmanaged, IConvertible
{
    bool ContainsFact(string key);

    T? RetrieveValue(string key);

    void AddFact(string key, T value);
}
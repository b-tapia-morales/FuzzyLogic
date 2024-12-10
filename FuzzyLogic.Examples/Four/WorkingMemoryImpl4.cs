using FuzzyLogic.Memory;

namespace FuzzyLogic.Examples.Four;

public static class WorkingMemoryImpl4
{
    public static IWorkingMemory Create(EntryResolutionMethod method = EntryResolutionMethod.Replace) =>
        WorkingMemory.Create(method, ("quality", 6), ("service", 9.8));
}
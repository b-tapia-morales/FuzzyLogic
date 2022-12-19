using FuzzyLogic.Memory;
using static FuzzyLogic.Memory.EntryResolutionMethod;

namespace FuzzyLogic.Test.Four;

public class WorkingMemoryImpl4 : WorkingMemory
{
    public new static IWorkingMemory Initialize(EntryResolutionMethod method = Replace)
    {
        var workingMemory = Create(method);
        workingMemory.AddFact("service", 3);
        workingMemory.AddFact("food", 8);
        return workingMemory;
    }
}
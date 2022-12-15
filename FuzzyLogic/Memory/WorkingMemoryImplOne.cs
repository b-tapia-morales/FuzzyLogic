using static FuzzyLogic.Memory.EntryResolutionMethod;

namespace FuzzyLogic.Memory;

public class WorkingMemoryImplOne : WorkingMemory
{
    public new static IWorkingMemory Initialize(EntryResolutionMethod method = Replace)
    {
        var workingMemory = Create();
        workingMemory.AddFact("Horario", 12);
        workingMemory.AddFact("Área", 150);
        workingMemory.AddFact("Espesor", 0.2);
        return workingMemory;
    }
}
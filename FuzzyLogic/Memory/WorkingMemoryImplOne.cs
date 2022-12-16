using static FuzzyLogic.Memory.EntryResolutionMethod;

namespace FuzzyLogic.Memory;

public class WorkingMemoryImplOne : WorkingMemory
{
    public new static IWorkingMemory Initialize(EntryResolutionMethod method = Replace)
    {
        var workingMemory = Create(method);
        workingMemory.AddFact("Horario", 7);
        workingMemory.AddFact("Área", 8);
        workingMemory.AddFact("Espesor", 0.06);
        return workingMemory;
    }
}
using FuzzyLogic.Memory;
using static FuzzyLogic.Memory.EntryResolutionMethod;

namespace FuzzyLogic.Test.Two;

public class WorkingMemoryImpl2 : WorkingMemory
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
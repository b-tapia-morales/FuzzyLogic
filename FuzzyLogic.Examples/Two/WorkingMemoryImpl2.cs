using FuzzyLogic.Memory;

namespace FuzzyLogic.Examples.Two;

public static class WorkingMemoryImpl2
{
    public static IWorkingMemory Initialize(EntryResolutionMethod method = EntryResolutionMethod.Replace)
    {
        var workingMemory = WorkingMemory.Create(method);
        workingMemory.AddFact("Horario", 7);
        workingMemory.AddFact("Área", 8);
        workingMemory.AddFact("Espesor", 0.06);
        return workingMemory;
    }
}
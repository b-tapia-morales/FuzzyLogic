using FuzzyLogic.Memory;

namespace FuzzyLogic.Examples.Three;

public static class WorkingMemoryImpl3
{
    public static IWorkingMemory Initialize(EntryResolutionMethod method = EntryResolutionMethod.Replace)
    {
        var workingMemory = WorkingMemory.Create(method);
        workingMemory.AddFact("Grasa", 20);
        workingMemory.AddFact("Azucar", 20);
        workingMemory.AddFact("Peso", 150);
        workingMemory.AddFact("Hidratacion", 50);
        return workingMemory;
    }
}
using FuzzyLogic.Memory;
using static FuzzyLogic.Memory.EntryResolutionMethod;

namespace FuzzyLogic.Test.Three;

public class WorkingMemoryImpl3 : WorkingMemory
{
    public new static IWorkingMemory Initialize(EntryResolutionMethod method = Replace)
    {
        var workingMemory = Create(method);
        workingMemory.AddFact("Grasa", 20);
        workingMemory.AddFact("Azucar", 20);
        workingMemory.AddFact("Peso", 150);
        workingMemory.AddFact("Hidratacion", 50);
        return workingMemory;
    }
}
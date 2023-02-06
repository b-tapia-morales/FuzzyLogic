using FuzzyLogic.Memory;
using static FuzzyLogic.Memory.EntryResolutionMethod;

namespace FuzzyLogic.Test.One;

public class TestWorkingMemoryImpl : WorkingMemory
{
    public new static IWorkingMemory Initialize(EntryResolutionMethod method = Preserve)
    {
        var workingMemory = Create(method);
        workingMemory.AddFact("Ret", 0.65);
        workingMemory.AddFact("Reg", 0.34);
        workingMemory.AddFact("Cnt", 0.34);
        workingMemory.AddFact("Dpj", 0.65);
        workingMemory.AddFact("Pc", 0.34);
        workingMemory.AddFact("Fz", 0.34);
        workingMemory.AddFact("Rea", 0.65);
        workingMemory.AddFact("Vlsp", 0.34);
        workingMemory.AddFact("Elmp", 0.65);
        workingMemory.AddFact("Vs", 0.34);
        workingMemory.AddFact("Ag", 0.65);
        workingMemory.AddFact("Sal", 0.34);
        return workingMemory;
    }
}
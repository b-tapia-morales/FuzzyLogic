using FuzzyLogic.Memory;

namespace FuzzyLogic.Test.One;

public class TestWorkingMemoryImpl: WorkingMemory
{
    private static readonly Random Random = new(Environment.TickCount);
    
    public new static IWorkingMemory Initialize()
    {
        var workingMemory = Create();
        workingMemory.AddFact("Ret", Random.NextDouble());
        workingMemory.AddFact("Reg", Random.NextDouble());
        workingMemory.AddFact("Cnt", Random.NextDouble());
        workingMemory.AddFact("Dpj", Random.NextDouble());
        workingMemory.AddFact("Pc", Random.NextDouble());
        workingMemory.AddFact("Fz", Random.NextDouble());
        workingMemory.AddFact("Rea", Random.NextDouble());
        workingMemory.AddFact("Vlsp", Random.NextDouble());
        workingMemory.AddFact("Elmp", Random.NextDouble());
        workingMemory.AddFact("Vs", Random.NextDouble());
        workingMemory.AddFact("Ag", Random.NextDouble());
        return workingMemory;
    }
}
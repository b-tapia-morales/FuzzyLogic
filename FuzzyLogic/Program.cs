using FuzzyLogic.Knowledge;
using FuzzyLogic.Linguistics;
using FuzzyLogic.Memory;
using FuzzyLogic.Proposition;
using FuzzyLogic.Proposition.Enums;
using FuzzyLogic.Rule;
using MathNet.Numerics.Integration;

var linguistics = LinguisticBase
    .Create()
    .AddLinguisticVariable(
        LinguisticVariable
            .Create("Water")
            .AddTrapezoidFunction("Cold", 0, 0, 20, 40)
            .AddTriangularFunction("Warm", 30, 50, 70)
            .AddTrapezoidFunction("Hot", 50, 80, 100, 100)
    ).AddLinguisticVariable(
        LinguisticVariable
            .Create("Power")
            .AddTriangularFunction("Low", 0, 25, 50)
            .AddTriangularFunction("High", 25, 50, 75)
    );

var rules = RuleBase
    .Create()
    .AddRule(
        FuzzyRule
            .Create()
            .If(FuzzyProposition.Is(linguistics, "Water", "Hot", HedgeToken.Slightly))
            .And(FuzzyProposition.Is(linguistics, "Power", "High"))
            .Then(FuzzyProposition.Is(linguistics, "Water", "Warm"))
    ).AddRule(
        FuzzyRule
            .Create()
            .If(FuzzyProposition.Is(linguistics, "Water", "Cold"))
            .And(FuzzyProposition.Is(linguistics, "Power", "Low"))
            .Then(FuzzyProposition.Is(linguistics, "Water", "Warm"))
    );

foreach (var rule in rules.ProductionRules)
    Console.WriteLine(rule);

var workingMemory = WorkingMemory.InitializeFromFile();
Console.WriteLine(workingMemory);

var function = linguistics.RetrieveLinguisticEntry("Water", "Cold")!.LambdaCutFunction(0.2);
var value = NewtonCotesTrapeziumRule.IntegrateAdaptive(function, 0, 40, 1e-10);
Console.WriteLine(value);

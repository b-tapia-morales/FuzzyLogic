using FuzzyLogic.Engine;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Memory;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition;
using FuzzyLogic.Proposition.Enums;
using FuzzyLogic.Rule;
using FuzzyLogic.Variable;
using MathNet.Numerics.Integration;

var linguistics = LinguisticBase
    .Create()
    .AddLinguisticVariable(
        LinguisticVariable
            .Create("Water", 0, 120)
            .AddTrapezoidFunction("Cold", 0, 0, 20, 40)
            .AddTriangularFunction("Warm", 30, 50, 70)
            .AddGaussianFunction("Hot", 80, 12)
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
            .Or(FuzzyProposition.Is(linguistics, "Water", "Warm", HedgeToken.Very))
            .And(FuzzyProposition.Is(linguistics, "Power", "High"))
            .Then(FuzzyProposition.Is(linguistics, "Power", "Low"))
    ).AddRule(
        FuzzyRule
            .Create()
            .If(FuzzyProposition.Is(linguistics, "Water", "Cold"))
            .And(FuzzyProposition.Is(linguistics, "Power", "Low"))
            .Then(FuzzyProposition.Is(linguistics, "Water", "Warm"))
    );

foreach (var rule in rules.ProductionRules)
    Console.WriteLine(rule);

var ruleCons = rules.FindRulesWithConclusion("Water");

foreach (var rule in ruleCons)
    Console.WriteLine(rule);

var workingMemory = WorkingMemory.InitializeFromFile(Path.Combine(Environment.CurrentDirectory, @"Files\Facts.csv"));
Console.WriteLine(workingMemory);

var function = linguistics.RetrieveLinguisticEntry("Water", "Hot")!;
var value = NewtonCotesTrapeziumRule.IntegrateAdaptive(function.LambdaCutFunction(0.5), 64, 116, 1e-10);
Console.WriteLine(value);

var facts = new Dictionary<string, double>
{
    ["Water"] = 52,
    ["Power"] = 30
};

var accessedRule = rules.ProductionRules.ElementAt(0);
Console.WriteLine(accessedRule);

var fuzzyNumbers = accessedRule.ApplyOperators(facts);
foreach (var fuzzyNumber in fuzzyNumbers)
{
    Console.WriteLine(fuzzyNumber);
}

var finalNumber = accessedRule.EvaluatePremiseWeight(facts);
Console.WriteLine(finalNumber ?? FuzzyNumber.MinValue());

var lambdaCut = accessedRule.ApplyImplication(facts);
Console.WriteLine(lambdaCut == null ? 0 : NewtonCotesTrapeziumRule.IntegrateAdaptive(lambdaCut, 0, 40, 1e-10));

var engine = InferenceEngine.Create(KnowledgeBase.Create(linguistics, rules), WorkingMemory.Create());

foreach (var variable in engine.ApplicableFromAvailableFacts())
{
    Console.WriteLine(variable);
}
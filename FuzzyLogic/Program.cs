using FuzzyLogic.Engine;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Memory;
using FuzzyLogic.Proposition;
using FuzzyLogic.Proposition.Enums;
using FuzzyLogic.Rule;
using FuzzyLogic.Tree;
using FuzzyLogic.Variable;

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
    ).AddLinguisticVariable(
        LinguisticVariable
            .Create("Temperature")
            .AddTriangularFunction("Low", 0, 10, 15)
            .AddTriangularFunction("Medium", 15, 20, 25)
            .AddTriangularFunction("High", 20, 25, 35)
    );

var rules = RuleBase
    .Create()
    .AddRule(
        FuzzyRule
            .Create()
            .If(FuzzyProposition.Is(linguistics, "Water", "Cold", HedgeToken.Very))
            .And(FuzzyProposition.Is(linguistics, "Power", "High"))
            .Then(FuzzyProposition.Is(linguistics, "Power", "Low"))
    ).AddRule(
        FuzzyRule
            .Create()
            .If(FuzzyProposition.Is(linguistics, "Water", "Warm"))
            .And(FuzzyProposition.Is(linguistics, "Power", "Low"))
            .Then(FuzzyProposition.Is(linguistics, "Power", "Low"))
    ).AddRule(
        FuzzyRule
            .Create()
            .If(FuzzyProposition.Is(linguistics, "Temperature", "Low"))
            .And(FuzzyProposition.Is(linguistics, "Water", "Cold"))
            .Then(FuzzyProposition.Is(linguistics, "Power", "High")));

var facts = new Dictionary<string, double>
{
    ["Water"] = 52,
    ["Power"] = 30
};

var memory = WorkingMemory.Create(facts);

var list = new List<IRealFunction>
{
    linguistics.RetrieveLinguisticEntry("Water", "Cold")!,
    linguistics.RetrieveLinguisticEntry("Water", "Warm")!,
    linguistics.RetrieveLinguisticEntry("Water", "Hot")!,
};

foreach (var function in list)
{
    Console.WriteLine(function.IsOpenLeft());
    Console.WriteLine(function.IsOpenRight());
    Console.WriteLine(function.IsClosed());
    Console.WriteLine(function.BoundaryInterval());
}

var set = InferenceEngine.CreateIntervalTable(list);

foreach (var tuple in set) Console.WriteLine($"{tuple.Key} - {string.Join(", ", tuple.Value.Select(e => e.Name))}");

var areas = InferenceEngine.CalculateArea(rules, memory, "Power");
foreach (var area in areas) Console.WriteLine(area);

var rootNode = TreeNode.CreateDerivationTree("Power", rules.ProductionRules);
Console.WriteLine(string.Join(Environment.NewLine, rootNode.Children.Select(e => e.VariableName)));
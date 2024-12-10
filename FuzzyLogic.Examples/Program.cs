using FuzzyLogic.Engine;
using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Examples.Four;
using FuzzyLogic.Utils;

var linguisticBase = LinguisticBaseImpl4.Create();
Console.WriteLine(linguisticBase);

var ruleBase = RuleBaseImpl4.Create(linguisticBase);

var workingMemory = WorkingMemoryImpl4.Create();

foreach (var fact in workingMemory.Facts)
{
    Console.WriteLine(fact);
}

foreach (var rule in ruleBase.ProductionRules)
{
    Console.WriteLine(rule);
    Console.WriteLine(rule.EvaluatePremiseWeight(workingMemory.Facts, Negation.RaisedCosine, Norm.Lukasiewicz, Conorm.Lukasiewicz));
}

var graph = ruleBase.GetDependencyGraph();
Console.WriteLine(string.Join(Environment.NewLine, graph.Select(x => $"{x.Key} : [{string.Join(", ", x.Value)}]")));

var inferenceEngine = InferenceEngine
    .Create(ruleBase, workingMemory)
    .UseDisjunction(Norm.Lukasiewicz)
    .UseConjunction(Conorm.ProbabilisticSum)
    .UseImplicationMethod(ImplicationMethod.Larsen)
    .UseDefuzzificationMethod(DefuzzificationMethod.MeanOfMaxima);

var value = inferenceEngine.Defuzzify("tip");
Console.WriteLine(value);
Console.WriteLine();
Console.WriteLine("Has the fact been successfully inferred?");
Console.WriteLine($"{(value == null ? "NO" : value)}");

var graphy = new Dictionary<string, IList<string>>
{
    {"A", ["B", "E"]},
    {"B", ["A", "C"]},
    {"C", ["D", "A"]},
    {"D", ["E"]},
    {"E", ["C"]}
};

var cycles = GraphUtils.FindCycles(graphy);

foreach (var cycle in cycles)
{
    Console.WriteLine(string.Join(", ", cycle));
}

var backEdges = GraphUtils.FindBackEdges(graphy);

foreach (var edge in backEdges)
{
    Console.WriteLine(edge);
}
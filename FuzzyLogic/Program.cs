using FuzzyLogic.Engine;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Number;
using FuzzyLogic.Test.One;
using static System.Globalization.CultureInfo;

var linguisticBase = TestLinguisticImpl.Initialize();

var ruleBase = TestRuleImpl.Initialize(linguisticBase);

var workingMemory = TestWorkingMemoryImpl.Initialize();

var knowledgeBase = KnowledgeBase<FuzzyNumber>.Create(linguisticBase, ruleBase);

var inferenceEngine = InferenceEngine<FuzzyNumber>.Create(knowledgeBase, workingMemory);

var value = inferenceEngine.Defuzzify("Hab");
Console.WriteLine();
Console.WriteLine("Has the fact been successfully inferred?");
Console.WriteLine($"{(value == null ? "NO" : value.Value.ToString(InvariantCulture))}");
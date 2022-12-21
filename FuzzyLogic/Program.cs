using FuzzyLogic.Engine;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Test.One;
using static System.Globalization.CultureInfo;
using static FuzzyLogic.Engine.Defuzzify.DefuzzificationMethod;

var linguisticBase = TestLinguisticImpl.Initialize();

var ruleBase = TestRuleImpl.Initialize(linguisticBase);

var workingMemory = TestWorkingMemoryImpl.Initialize();

var knowledgeBase = KnowledgeBase.Create(linguisticBase, ruleBase);

var inferenceEngine = InferenceEngine.Create(knowledgeBase, workingMemory, MeanOfMaxima);

var value = inferenceEngine.Defuzzify("Hab");
Console.WriteLine();
Console.WriteLine("Has the fact been successfully inferred?");
Console.WriteLine($"{(value == null ? "NO" : value.Value.ToString(InvariantCulture))}");
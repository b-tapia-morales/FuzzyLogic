using FuzzyLogic.Engine;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Test.One;
using static System.Globalization.CultureInfo;
using static FuzzyLogic.Engine.Defuzzify.DefuzzificationMethod;
using static FuzzyLogic.Memory.EntryResolutionMethod;
using static FuzzyLogic.Rule.ComparingMethod;

var linguisticBase = TestLinguisticImpl.Initialize();

var ruleBase = TestRuleImpl.Initialize(linguisticBase, ShortestPremise);

var workingMemory = TestWorkingMemoryImpl.Initialize(Replace);

var knowledgeBase = KnowledgeBase.Create(linguisticBase, ruleBase);

var inferenceEngine = InferenceEngine.Create(knowledgeBase, workingMemory, CentreOfArea);

var value = inferenceEngine.Defuzzify("Hab");
Console.WriteLine();
Console.WriteLine($"Has the fact been successfully inferred? {value?.ToString(InvariantCulture)}");
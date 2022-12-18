using FuzzyLogic.Engine;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Test.One;
using FuzzyLogic.Tree;
using static System.Globalization.CultureInfo;
using static FuzzyLogic.Engine.Defuzzify.DefuzzificationMethod;
using static FuzzyLogic.Memory.EntryResolutionMethod;
using static FuzzyLogic.Rule.ComparingMethod;

var linguisticBase = TestLinguisticImpl.Initialize();

var ruleBase = TestRuleImpl.Initialize(linguisticBase, ShortestPremise);

var workingMemory = TestWorkingMemoryImpl.Initialize(Replace);

var knowledgeBase = KnowledgeBase.Create(linguisticBase, ruleBase);

var inferenceEngine = InferenceEngine.Create(knowledgeBase, workingMemory, CentreOfArea);

var rootNode =
    TreeNode.CreateDerivationTree("Hab", ruleBase.ProductionRules, ruleBase.RuleComparer, workingMemory.Facts);
rootNode.WriteTree();
var value = rootNode.InferFact(workingMemory.Facts, inferenceEngine.Defuzzifier);
Console.WriteLine(value == null ? "NADA" : value.GetValueOrDefault().ToString(InvariantCulture));
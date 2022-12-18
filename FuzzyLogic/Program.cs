using FuzzyLogic.Engine;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Test.One;
using FuzzyLogic.Tree;
using static FuzzyLogic.Engine.Defuzzify.DefuzzificationMethod;
using static FuzzyLogic.Rule.ComparingMethod;

var linguisticBase = TestLinguisticImpl.Initialize();

var ruleBase = TestRuleImpl.Initialize(linguisticBase, LargestPremise);

var workingMemory = TestWorkingMemoryImpl.Initialize();

var knowledgeBase = KnowledgeBase.Create(linguisticBase, ruleBase);

var inferenceEngine = InferenceEngine.Create(knowledgeBase, workingMemory, FirstOfMaxima);

var rootNode =
    TreeNode.CreateDerivationTree("Hab", ruleBase.ProductionRules, ruleBase.RuleComparer, workingMemory.Facts);
TreeNode.DisplayDerivationTree(rootNode);
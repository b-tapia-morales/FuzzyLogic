using FuzzyLogic.Engine;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Test.One;
using FuzzyLogic.Test.Two;
using FuzzyLogic.Tree;

var linguisticBase = TestLinguisticImpl.Initialize();

var ruleBase = TestRuleImpl.Initialize(linguisticBase);

var workingMemory = WorkingMemoryImpl2.Initialize();

var knowledgeBase = KnowledgeBase.Create(linguisticBase, ruleBase);

var inferenceEngine = InferenceEngine.Create(knowledgeBase, workingMemory);

var rootNode = TreeNode.CreateDerivationTree("Def", ruleBase.ProductionRules, workingMemory.Facts);
Console.WriteLine(string.Join(Environment.NewLine, rootNode.Rules));
Console.WriteLine();
Console.WriteLine(string.Join(Environment.NewLine, rootNode.Children.Select(e => e.VariableName)));
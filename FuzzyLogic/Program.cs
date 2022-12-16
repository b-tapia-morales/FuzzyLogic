using FuzzyLogic.Engine;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Memory;
using FuzzyLogic.Tree;

var linguisticBase = LinguisticBaseImplOne.Initialize();

var ruleBase = RuleBaseImplOne.Initialize(linguisticBase);

var workingMemory = WorkingMemoryImplOne.Initialize();

var knowledgeBase = KnowledgeBase.Create(linguisticBase, ruleBase);

var inferenceEngine = InferenceEngine.Create(knowledgeBase, workingMemory);

var rootNode = TreeNode.CreateDerivationTree("Densidad de corriente", ruleBase.ProductionRules, workingMemory.Facts);
Console.WriteLine(string.Join(Environment.NewLine, rootNode.Rules));
Console.WriteLine();
Console.WriteLine(string.Join(Environment.NewLine, rootNode.Children.Select(e => e.VariableName)));
using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Tree;

public class TreeNode : ITreeNode<TreeNode>
{
    public string VariableName { get; }
    public ICollection<IRule> Rules { get; }
    public ICollection<ITreeNode<TreeNode>> Children { get; }
    public bool IsProven { get; set; }

    public TreeNode(string variableName)
    {
        VariableName = variableName;
        Rules = new List<IRule>();
        Children = new List<ITreeNode<TreeNode>>();
        IsProven = false;
    }

    public bool IsLeaf() => !Children.Any();

    public void AddRules(IEnumerable<IRule> rules)
    {
        foreach (var rule in rules)
            Rules.Add(rule);
    }

    public void AddChild(ITreeNode<TreeNode> child) => Children.Add(child);

    public void AddChildren(IEnumerable<ITreeNode<TreeNode>> children)
    {
        foreach (var child in children)
            Children.Add(child);
    }

    public void Display()
    {
        var isFact = IsLeaf();
        Console.Write(isFact ? "Leaf node: " : "Parent node: ");
        Console.WriteLine(VariableName);
        if (!isFact)
            Console.WriteLine(string.Join(Environment.NewLine, Rules));
    }

    public static ITreeNode<TreeNode> CreateDerivationTree(string variableName, ICollection<IRule> rules,
        IComparer<IRule> ruleComparer, IDictionary<string, double> facts)
    {
        var rootNode = new TreeNode(variableName);
        var stack = new Stack<ITreeNode<TreeNode>>();
        stack.Push(rootNode);
        var circularDependencies = new Stack<string>();
        while (stack.TryPop(out var node))
        {
            circularDependencies.TryPop(out _);
            UpdateNode(node, node.VariableName, rules, ruleComparer, facts, circularDependencies);
            foreach (var child in node.Children)
                stack.Push(child);
            circularDependencies.Push(node.VariableName);
        }

        return rootNode;
    }

    public static double? DeriveFacts(ITreeNode<TreeNode> rootNode, IDictionary<string, double> facts,
        IDefuzzifier defuzzifier)
    {
        var queue = new Queue<ITreeNode<TreeNode>>();
        queue.Enqueue(rootNode);
        var stack = new Stack<ITreeNode<TreeNode>>();
        while (queue.TryDequeue(out var node))
        {
            stack.Push(node);
            foreach (var child in node.Children)
                queue.Enqueue(child);
        }

        while (stack.TryPop(out var node))
        {
            if (node.IsLeaf())
            {
                node.IsProven = facts.ContainsKey(node.VariableName);
                continue;
            }

            var applicableRules = node.Rules.Where(e => e.IsApplicable(facts)).ToList();
            if (!applicableRules.Any())
            {
                node.IsProven = false;
                continue;
            }

            var value = defuzzifier.Defuzzify(applicableRules, facts).GetValueOrDefault();
            facts.TryAdd(node.VariableName, value);
            node.IsProven = true;
        }

        return facts.TryGetValue(rootNode.VariableName, out var expected) ? expected : null;
    }

    public static void DisplayDerivationTree(ITreeNode<TreeNode> rootNode)
    {
        var stack = new Stack<ITreeNode<TreeNode>>();
        stack.Push(rootNode);
        while (stack.TryPop(out var node))
        {
            node.Display();
            foreach (var child in node.Children)
                stack.Push(child);
        }
    }

    private static void UpdateNode(ITreeNode<TreeNode> node, string variableName, ICollection<IRule> rules,
        IComparer<IRule> ruleComparer, IDictionary<string, double> facts, Stack<string> circularDependencies)
    {
        if (facts.ContainsKey(variableName))
            return;
        var filteredRules = RuleBase.FilterByResolutionMethod(variableName, rules, ruleComparer);
        if (!filteredRules.Any())
            return;
        var antecedents = filteredRules
            .Select(e => e.Antecedent!.LinguisticVariable.Name)
            //.Where(e => !circularDependencies.Contains(e))
            .ToList();
        var connectives = filteredRules
            .SelectMany(e => e.Connectives)
            .Select(e => e.LinguisticVariable.Name)
            //.Where(e => !circularDependencies.Contains(e))
            .ToList();
        var set = new HashSet<string>();
        set.UnionWith(antecedents);
        set.UnionWith(connectives);
        var children = new List<ITreeNode<TreeNode>>(set.Select(e => new TreeNode(e)));
        node.AddRules(filteredRules);
        node.AddChildren(children);
    }
}
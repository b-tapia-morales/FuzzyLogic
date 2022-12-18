using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Tree;

public class TreeNode : ITreeNode<TreeNode>
{
    public string VariableName { get; }
    public ICollection<IRule> Rules { get; }
    public ICollection<TreeNode> Children { get; }
    public bool IsProven { get; set; }

    public TreeNode(string variableName)
    {
        VariableName = variableName;
        Rules = new List<IRule>();
        Children = new List<TreeNode>();
        IsProven = false;
    }

    public bool IsLeaf() => !Children.Any();

    public void AddRules(IEnumerable<IRule> rules)
    {
        foreach (var rule in rules)
            Rules.Add(rule);
    }

    public void AddChild(TreeNode child) => Children.Add(child);

    public void AddChildren(IEnumerable<TreeNode> children)
    {
        foreach (var child in children)
            Children.Add(child);
    }

    public void WriteNode()
    {
        Console.WriteLine($"Linguistic variable name: {VariableName}");
        if (!IsLeaf())
            Console.WriteLine($"Associated rules:{Environment.NewLine}{string.Join(Environment.NewLine, Rules)}");
        Console.WriteLine($"Is it proven? {IsProven}");
    }

    public void WriteTree() => WriteTree(this);

    public void PrettyWriteTree() => PrettyWriteTree(this);

    public double? InferFact(IDictionary<string, double> facts, IDefuzzifier defuzzifier) =>
        InferFact(this, facts, defuzzifier);

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

    private static void WriteTree(ITreeNode<TreeNode> rootNode)
    {
        var stack = new Stack<ITreeNode<TreeNode>>();
        stack.Push(rootNode);
        while (stack.TryPop(out var node))
        {
            node.WriteNode();
            foreach (var child in node.Children)
                stack.Push(child);
        }
    }

    private static void PrettyWriteTree(ITreeNode<TreeNode> node, string indent = "", bool last = true)
    {
        Console.WriteLine(indent + "+- " + node.VariableName);
        indent += last ? "   " : "|  ";
        for (var i = 0; i < node.Children.Count; i++)
            PrettyWriteTree(node.Children.ElementAt(i), indent, i == node.Children.Count - 1);
    }

    private static double? InferFact(ITreeNode<TreeNode> rootNode, IDictionary<string, double> facts,
        IDefuzzifier defuzzifier)
    {
        var stack = TraverseReverseLevelOrder(rootNode);
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

            var crispValue = defuzzifier.Defuzzify(applicableRules, facts);
            if (crispValue != null)
                facts.TryAdd(node.VariableName, crispValue.Value);
            node.IsProven = true;
        }

        if (facts.TryGetValue(rootNode.VariableName, out var fact))
            rootNode.IsProven = true;
        return fact;
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
        var children = new List<TreeNode>(set.Select(e => new TreeNode(e)));
        node.AddRules(filteredRules);
        node.AddChildren(children);
    }

    private static Stack<ITreeNode<TreeNode>> TraverseReverseLevelOrder(ITreeNode<TreeNode> rootNode)
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

        return stack;
    }
}
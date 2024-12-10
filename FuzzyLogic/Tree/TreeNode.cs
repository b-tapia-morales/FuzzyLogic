using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Enum.Family;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Tree;

public class TreeNode
{
    public string VariableName { get; }
    public ICollection<IRule> Rules { get; } = new List<IRule>();
    public ICollection<TreeNode> Children { get; } = new List<TreeNode>();
    public bool IsProven { get; private set; }

    private TreeNode(string variableName) => VariableName = variableName;

    public bool IsLeaf() => Children.Count == 0;

    private void AddRules(IEnumerable<IRule> rules)
    {
        foreach (var rule in rules)
            Rules.Add(rule);
    }

    private void AddChildren(IEnumerable<TreeNode> children)
    {
        foreach (var child in children)
            Children.Add(child);
    }

    public double? InferFact(IDictionary<string, double> facts, IDefuzzifier defuzzifier, IOperatorFamily operatorFamily,
        ImplicationMethod method = ImplicationMethod.Mamdani) =>
        InferFact(this, facts, defuzzifier, operatorFamily, method);

    public static TreeNode CreateDerivationTree(string variableName, ICollection<IRule> rules,
        IComparer<IRule> ruleComparer, IDictionary<string, double> facts)
    {
        var rootNode = new TreeNode(variableName);
        var stack = new Stack<TreeNode>();
        stack.Push(rootNode);
        while (stack.TryPop(out var node))
        {
            UpdateNode(node, node.VariableName, rules, ruleComparer, facts);
            foreach (var child in node.Children)
                stack.Push(child);
        }

        return rootNode;
    }

    private static double? InferFact(TreeNode rootNode, IDictionary<string, double> facts,
        IDefuzzifier defuzzifier, IOperatorFamily operatorFamily, ImplicationMethod method = ImplicationMethod.Mamdani)
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
            if (applicableRules.Count == 0)
            {
                node.IsProven = false;
                continue;
            }

            var crispValue = defuzzifier.Defuzzify(applicableRules, facts, operatorFamily, method);
            if (crispValue != null)
                facts.TryAdd(node.VariableName, crispValue.Value);
            node.IsProven = true;
        }

        if (facts.TryGetValue(rootNode.VariableName, out var fact))
            rootNode.IsProven = true;
        return fact;
    }

    private static void UpdateNode(TreeNode node, string variableName, ICollection<IRule> rules,
        IComparer<IRule> ruleComparer, IDictionary<string, double> facts)
    {
        if (facts.ContainsKey(variableName))
            return;
        var filteredRules = RuleBase.FilterByResolutionMethod(rules, variableName, ruleComparer);
        if (filteredRules.Count == 0)
            return;
        var antecedents = filteredRules.Select(e => e.Conditional!.VariableName).ToList();
        var connectives = filteredRules.SelectMany(e => e.Connectives).Select(e => e.VariableName).ToList();
        var set = new HashSet<string>(antecedents.Union(connectives));
        var children = new List<TreeNode>(set.Select(e => new TreeNode(e)));
        node.AddRules(filteredRules);
        node.AddChildren(children);
    }

    private static Stack<TreeNode> TraverseReverseLevelOrder(TreeNode rootNode)
    {
        var queue = new Queue<TreeNode>();
        queue.Enqueue(rootNode);
        var stack = new Stack<TreeNode>();
        while (queue.TryDequeue(out var node))
        {
            stack.Push(node);
            foreach (var child in node.Children)
                queue.Enqueue(child);
        }

        return stack;
    }
}

public static class TreeExtensions
{
    public static Stack<TreeNode> TraverseReverseLevelOrder(this TreeNode rootNode)
    {
        var queue = new Queue<TreeNode>();
        queue.Enqueue(rootNode);
        var stack = new Stack<TreeNode>();
        while (queue.TryDequeue(out var node))
        {
            stack.Push(node);
            foreach (var child in node.Children)
                queue.Enqueue(child);
        }

        return stack;
    }

    public static void PrettyWriteTree(this TreeNode node, string indent = "", bool last = true)
    {
        Console.WriteLine($"{indent}+- {node.VariableName}");
        indent += last ? "   " : "|  ";
        for (var i = 0; i < node.Children.Count; i++)
            PrettyWriteTree(node.Children.ElementAt(i), indent, i == node.Children.Count - 1);
    }

    public static void WriteTree(this TreeNode rootNode)
    {
        var stack = new Stack<TreeNode>();
        stack.Push(rootNode);
        while (stack.TryPop(out var node))
        {
            node.WriteNode();
            foreach (var child in node.Children)
                stack.Push(child);
        }
    }

    private static void WriteNode(this TreeNode node)
    {
        Console.WriteLine($"Linguistic variable name: {node.VariableName}");
        if (!node.IsLeaf())
            Console.WriteLine($"Associated rules:{Environment.NewLine}{string.Join(Environment.NewLine, node.Rules)}");
        Console.WriteLine($"Is it proven? {node.IsProven}");
    }
}
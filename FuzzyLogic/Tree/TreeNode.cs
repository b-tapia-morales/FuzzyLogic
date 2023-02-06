using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Number;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Tree;

public class TreeNode<T> where T : struct, IFuzzyNumber<T>
{
    public string VariableName { get; }
    public ICollection<IRule<T>> Rules { get; } = new List<IRule<T>>();
    public ICollection<TreeNode<T>> Children { get; } = new List<TreeNode<T>>();
    public bool IsProven { get; set; }

    public TreeNode(string variableName) => VariableName = variableName;

    public bool IsLeaf() => !Children.Any();

    public void AddRules(IEnumerable<IRule<T>> rules)
    {
        foreach (var rule in rules)
            Rules.Add(rule);
    }

    public void AddChild(TreeNode<T> child) => Children.Add(child);

    public void AddChildren(IEnumerable<TreeNode<T>> children)
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

    public double? InferFact(IDictionary<string, double> facts, IDefuzzifier<T> defuzzifier) =>
        InferFact(this, facts, defuzzifier);

    public static TreeNode<T> CreateDerivationTree(string variableName, ICollection<IRule<T>> rules,
        IComparer<IRule<T>> ruleComparer, IDictionary<string, double> facts)
    {
        var rootNode = new TreeNode<T>(variableName);
        var stack = new Stack<TreeNode<T>>();
        stack.Push(rootNode);
        var circularDependencies = new LinkedList<string>();
        while (stack.TryPop(out var node))
        {
            if (circularDependencies.Any())
                circularDependencies.RemoveFirst();
            UpdateNode(node, node.VariableName, rules, ruleComparer, facts, circularDependencies);
            foreach (var child in node.Children)
                stack.Push(child);
            circularDependencies.AddFirst(node.VariableName);
        }

        return rootNode;
    }

    private static void WriteTree(TreeNode<T> rootNode)
    {
        var stack = new Stack<TreeNode<T>>();
        stack.Push(rootNode);
        while (stack.TryPop(out var node))
        {
            node.WriteNode();
            foreach (var child in node.Children)
                stack.Push(child);
        }
    }

    private static void PrettyWriteTree(TreeNode<T> node, string indent = "", bool last = true)
    {
        Console.WriteLine(indent + "+- " + node.VariableName);
        indent += last ? "   " : "|  ";
        for (var i = 0; i < node.Children.Count; i++)
            PrettyWriteTree(node.Children.ElementAt(i), indent, i == node.Children.Count - 1);
    }

    private static double? InferFact(TreeNode<T> rootNode, IDictionary<string, double> facts,
        IDefuzzifier<T> defuzzifier)
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

    private static void UpdateNode(TreeNode<T> node, string variableName, ICollection<IRule<T>> rules,
        IComparer<IRule<T>> ruleComparer, IDictionary<string, double> facts, ICollection<string> circularDependencies)
    {
        if (facts.ContainsKey(variableName))
            return;
        var filteredRules = RuleBase<T>.FilterByResolutionMethod(variableName, rules, ruleComparer);
        if (!filteredRules.Any())
            return;
        var antecedents = filteredRules
            .Select(e => e.Antecedent!.LinguisticVariable.Name)
            .Where(e => !circularDependencies.Contains(e))
            .ToList();
        var connectives = filteredRules
            .SelectMany(e => e.Connectives)
            .Select(e => e.LinguisticVariable.Name)
            .Where(e => !circularDependencies.Contains(e))
            .ToList();
        var set = new HashSet<string>();
        set.UnionWith(antecedents);
        set.UnionWith(connectives);
        var children = new List<TreeNode<T>>(set.Select(e => new TreeNode<T>(e)));
        node.AddRules(filteredRules);
        node.AddChildren(children);
    }

    private static Stack<TreeNode<T>> TraverseReverseLevelOrder(TreeNode<T> rootNode)
    {
        var queue = new Queue<TreeNode<T>>();
        queue.Enqueue(rootNode);
        var stack = new Stack<TreeNode<T>>();
        while (queue.TryDequeue(out var node))
        {
            stack.Push(node);
            foreach (var child in node.Children)
                queue.Enqueue(child);
        }

        return stack;
    }
}
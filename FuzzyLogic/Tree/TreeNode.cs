using FuzzyLogic.Rule;

namespace FuzzyLogic.Tree;

public class TreeNode : ITreeNode<TreeNode>
{
    public string VariableName { get; }
    public bool IsFact { get; set; }
    public ICollection<IRule> Rules { get; }
    public ICollection<ITreeNode<TreeNode>> Children { get; }

    public TreeNode(string variableName)
    {
        VariableName = variableName;
        IsFact = false;
        Rules = new List<IRule>();
        Children = new List<ITreeNode<TreeNode>>();
    }

    public bool IsLeaf() => Children.Any();

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
        Console.WriteLine(VariableName);
        if (!IsFact)
            Console.WriteLine(string.Join(Environment.NewLine, Rules));
    }

    public static ITreeNode<TreeNode> CreateDerivationTree(string variableName, ICollection<IRule> rules,
        IDictionary<string, double> facts)
    {
        var rootNode = new TreeNode(variableName);
        var stack = new Stack<ITreeNode<TreeNode>>();
        stack.Push(rootNode);
        var circularDependencies = new Stack<string>();
        while (stack.TryPop(out var node))
        {
            //circularDependencies.Push(node.VariableName);
            UpdateNode(ref node, node.VariableName, rules, facts, circularDependencies);
            foreach (var child in node.Children)
                stack.Push(child);
        }

        return rootNode;
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

    private static void UpdateNode(ref ITreeNode<TreeNode> node, string variableName, ICollection<IRule> rules,
        IDictionary<string, double> facts, Stack<string> circularDependencies)
    {
        if (facts.ContainsKey(variableName))
        {
            node.IsFact = true;
            return;
        }

        var filteredRules = rules
            .Where(e => e.ConclusionContainsVariable(variableName))
            .ToList();
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
using FuzzyLogic.Rule;

namespace FuzzyLogic.Tree;

public class TreeNode : ITreeNode<TreeNode>
{
    public string VariableName { get; }
    public ICollection<IRule> Rules { get; }
    public ICollection<ITreeNode<TreeNode>> Children { get; }

    public TreeNode(string variableName)
    {
        VariableName = variableName;
        Rules = new List<IRule>();
        Children = new List<ITreeNode<TreeNode>>();
    }

    public TreeNode(string variableName, ICollection<IRule> rules, ICollection<ITreeNode<TreeNode>> children)
    {
        VariableName = variableName;
        Rules = rules;
        Children = children;
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

    public static ITreeNode<TreeNode> CreateDerivationTree(string variableName, ICollection<IRule> rules)
    {
        var circularDependencies = new HashSet<string>();
        var rootNode = new TreeNode(variableName);
        var stack = new Stack<ITreeNode<TreeNode>>();
        stack.Push(rootNode);
        while (stack.TryPop(out var node))
        {
            circularDependencies.Add(node.VariableName);
            UpdateNode(ref node, node.VariableName, rules, circularDependencies);
            foreach (var child in node.Children)
            {
                //Console.WriteLine(node.VariableName);
                stack.Push(child);
            }
        }

        return rootNode;
    }

    private static void UpdateNode(ref ITreeNode<TreeNode> node, string variableName, ICollection<IRule> rules,
        ISet<string> circularDependencies)
    {
        var filteredRules = rules
            .Where(e => e.ConclusionContainsVariable(variableName))
            .ToList();
        var dependencies = filteredRules
            .Select(e => e.Antecedent!.LinguisticVariable.Name)
            .Where(e => !circularDependencies.Contains(e))
            .Distinct()
            .ToList();
        var children = new List<ITreeNode<TreeNode>>(dependencies.Select(e => new TreeNode(e)));
        node.AddRules(rules);
        node.AddChildren(children);
    }
}
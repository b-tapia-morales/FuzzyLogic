using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Tree;

public interface ITreeNode<T> where T : class, ITreeNode<T>
{
    string VariableName { get; }
    ICollection<IRule> Rules { get; }
    ICollection<ITreeNode<TreeNode>> Children { get; }
    bool IsProven { get; set; }

    bool IsLeaf();

    void AddRules(IEnumerable<IRule> rules);

    void AddChild(ITreeNode<T> child);

    void AddChildren(IEnumerable<ITreeNode<T>> children);

    void WriteNode();

    void WriteTree();

    double? InferFact(IDictionary<string, double> facts, IDefuzzifier defuzzifier);
}
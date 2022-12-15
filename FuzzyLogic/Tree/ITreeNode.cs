using FuzzyLogic.Rule;

namespace FuzzyLogic.Tree;

public interface ITreeNode<T> where T : class, ITreeNode<T>
{
    string VariableName { get; }
    ICollection<IRule> Rules { get; }
    ICollection<ITreeNode<T>> Children { get; }

    bool IsLeaf();

    void AddRules(IEnumerable<IRule> rules);

    void AddChild(ITreeNode<T> child);

    void AddChildren(IEnumerable<ITreeNode<T>> children);
}
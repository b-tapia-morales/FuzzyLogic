using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Tree;

public interface ITreeNode<T> where T : class, ITreeNode<T>
{
    string VariableName { get; }
    ICollection<IRule> Rules { get; }
    ICollection<T> Children { get; }
    bool IsProven { get; set; }

    bool IsLeaf();

    void AddRules(IEnumerable<IRule> rules);

    void AddChild(T child);

    void AddChildren(IEnumerable<T> children);

    void WriteNode();

    void WriteTree();

    void PrettyWriteTree();

    double? InferFact(IDictionary<string, double> facts, IDefuzzifier defuzzifier);
}
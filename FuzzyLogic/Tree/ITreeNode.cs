using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Enum.Family;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Tree;

public interface ITreeNode<T> where T : class, ITreeNode<T>
{
    string VariableName { get; }
    ICollection<T> Children { get; }
    ICollection<IRule> Rules { get; }
    bool IsProven { get; }

    bool IsLeaf();

    void AddChildren(params IEnumerable<T> children);

    void AddRules(params IEnumerable<IRule> rules);

    double? InferFact(IDictionary<string, double> facts, IDefuzzifier defuzzifier, IOperatorFamily operatorFamily,
        ImplicationMethod method = ImplicationMethod.Mamdani);

    static abstract ICollection<IRule> FindApplicableRules(string variableName, ICollection<IRule> rules, IComparer<IRule> comparer);

    static abstract T CreateDerivationTree(string variableName, ICollection<IRule> rules, IComparer<IRule> comparer,
        IDictionary<string, double> facts);
}
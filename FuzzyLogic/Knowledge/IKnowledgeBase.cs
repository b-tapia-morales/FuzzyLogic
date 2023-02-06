using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Number;

namespace FuzzyLogic.Knowledge;

public interface IKnowledgeBase<T> where T : struct, IFuzzyNumber<T>
{
    ILinguisticBase LinguisticBase { get; }
    IRuleBase<T> RuleBase { get; }

    IKnowledgeBase<T> Clone();
    
    static abstract IKnowledgeBase<T> Create();
    
    static abstract IKnowledgeBase<T> Create(ILinguisticBase linguisticBase, IRuleBase<T> ruleBase);
}
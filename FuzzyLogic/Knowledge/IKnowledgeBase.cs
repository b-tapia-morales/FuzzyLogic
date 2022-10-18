namespace FuzzyLogic.Knowledge;

public interface IKnowledgeBase
{
    ILinguisticBase LinguisticBase { get; }
    IRuleBase RuleBase { get; }

    IKnowledgeBase Clone();
    
    static abstract IKnowledgeBase Create();
    
    static abstract IKnowledgeBase Create(ILinguisticBase linguisticBase, IRuleBase ruleBase);
}
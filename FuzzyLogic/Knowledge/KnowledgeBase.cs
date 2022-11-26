using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;

namespace FuzzyLogic.Knowledge;

public class KnowledgeBase : IKnowledgeBase
{
    public ILinguisticBase LinguisticBase { get; }
    public IRuleBase RuleBase { get; }

    protected KnowledgeBase()
    {
        LinguisticBase = Linguistic.LinguisticBase.Create();
        RuleBase = Rule.RuleBase.Create();
    }

    private KnowledgeBase(ILinguisticBase linguisticBase, IRuleBase ruleBase)
    {
        LinguisticBase = linguisticBase;
        RuleBase = ruleBase;
    }

    public IKnowledgeBase Clone() => (KnowledgeBase) MemberwiseClone();

    public static IKnowledgeBase Create() => new KnowledgeBase();

    public static IKnowledgeBase Create(ILinguisticBase linguisticBase, IRuleBase ruleBase) =>
        new KnowledgeBase(linguisticBase, ruleBase);
}
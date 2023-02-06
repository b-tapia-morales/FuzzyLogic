using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Number;

namespace FuzzyLogic.Knowledge;

public class KnowledgeBase<T>: IKnowledgeBase<T> where T : struct, IFuzzyNumber<T>
{
    public ILinguisticBase LinguisticBase { get; }
    public IRuleBase<T> RuleBase { get; }

    protected KnowledgeBase()
    {
        LinguisticBase = Linguistic.LinguisticBase.Create();
        RuleBase = RuleBase<T>.Create();
    }

    private KnowledgeBase(ILinguisticBase linguisticBase, IRuleBase<T> ruleBase)
    {
        LinguisticBase = linguisticBase;
        RuleBase = ruleBase;
    }

    public IKnowledgeBase<T> Clone() => (KnowledgeBase<T>) MemberwiseClone();

    public static IKnowledgeBase<T> Create() => new KnowledgeBase<T>();

    public static IKnowledgeBase<T> Create(ILinguisticBase linguisticBase, IRuleBase<T> ruleBase) =>
        new KnowledgeBase<T>(linguisticBase, ruleBase);
}
using FuzzyLogic.Rule;

namespace FuzzyLogic.Knowledge;

public class KnowledgeBase : IKnowledgeBase
{
    public ICollection<IRule> ProductionRules { get; set; } = new List<IRule>();

    public void ExcludeIncompleteRules() => ExcludeIncompleteRules(this);

    public void ExcludeRulesWithUnavailableVariables(IDictionary<string, double> facts) =>
        throw new NotImplementedException();

    public void ExcludeRulesWithUnavailableValues(IDictionary<string, double> facts) =>
        throw new NotImplementedException();

    public static IKnowledgeBase ExcludeIncompleteRules(IKnowledgeBase knowledgeBase)
    {
        knowledgeBase.ProductionRules = knowledgeBase.ProductionRules.Where(e => e.Consequent != null).ToList();
        return knowledgeBase;
    }

    public static IKnowledgeBase ExcludeRulesWithUnavailableVariables(IKnowledgeBase knowledgeBase,
        IDictionary<string, double> facts) => throw new NotImplementedException();

    public static IKnowledgeBase ExcludeRulesWithUnavailableValues(IKnowledgeBase knowledgeBase,
        IDictionary<string, double> facts) => throw new NotImplementedException();
}
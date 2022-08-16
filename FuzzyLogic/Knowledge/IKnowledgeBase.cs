using FuzzyLogic.Rule;

namespace FuzzyLogic.Knowledge;

public interface IKnowledgeBase
{
    public ICollection<IRule> ProductionRules { get; set; }

    public void ExcludeIncompleteRules();
    
    public void ExcludeRulesWithUnavailableVariables(IDictionary<string, double> facts);
    
    public void ExcludeRulesWithUnavailableValues(IDictionary<string, double> facts);
}
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Examples.Four;

public static class RuleBaseImpl4
{
    public static IRuleBase Create(ILinguisticBase linguisticBase,
        ComparingMethod method = ComparingMethod.HighestPriority)
    {
        var r1 = FuzzyRule.Create(linguisticBase)
            .If("quality", "bad")
            .Or("service", "poor")
            .Then("tip", "low");
        var r2 = FuzzyRule.Create(linguisticBase)
            .If("service", "acceptable")
            .Then("tip", "medium");
        var r3 = FuzzyRule.Create(linguisticBase)
            .If("quality", "great")
            .Or("service", "amazing")
            .Then("tip", "high");
        return RuleBase.Create(method, r1, r2, r3);
    }
}
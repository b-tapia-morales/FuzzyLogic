using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;
using static FuzzyLogic.Proposition.FuzzyProposition;
using static FuzzyLogic.Rule.ComparingMethod;

namespace FuzzyLogic.Test.Four;

public class RuleBaseImpl4 : RuleBase
{
    public new static IRuleBase Initialize(ILinguisticBase @base, ComparingMethod method = Priority)
    {
        var r1 = FuzzyRule.Create()
            .If(@base.Is("service", "poor"))
            .Or(@base.Is("food", "rancid"))
            .Then(@base.Is("tip", "cheap"));
        var r2 = FuzzyRule.Create()
            .If(@base.Is("service", "good"))
            .Then(@base.Is("tip", "average"));
        var r3 = FuzzyRule.Create()
            .If(@base.Is("service", "excellent"))
            .Or(@base.Is("food", "delicious"))
            .Then(@base.Is("tip", "generous"));
        return Create(method).AddAll(r1, r2, r3);
    }
}
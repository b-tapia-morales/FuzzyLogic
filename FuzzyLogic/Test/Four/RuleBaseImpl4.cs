using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;
using static FuzzyLogic.Proposition.FuzzyProposition;
using static FuzzyLogic.Rule.ComparingMethod;

namespace FuzzyLogic.Test.Four;

public class RuleBaseImpl4 : RuleBase
{
    public new static IRuleBase Initialize(ILinguisticBase linguisticBase, ComparingMethod method = ComparingMethod.HighestPriority)
    {
        var r1 = FuzzyRule.Create()
            .If(Is(linguisticBase, "service", "poor"))
            .Or(Is(linguisticBase, "food", "rancid"))
            .Then(Is(linguisticBase, "tip", "cheap"));
        var r2 = FuzzyRule.Create()
            .If(Is(linguisticBase, "service", "good"))
            .Then(Is(linguisticBase, "tip", "average"));
        var r3 = FuzzyRule.Create()
            .If(Is(linguisticBase, "service", "excellent"))
            .Or(Is(linguisticBase, "food", "delicious"))
            .Then(Is(linguisticBase, "tip", "generous"));
        return Create(method).AddAll(r1, r2, r3);
    }
}
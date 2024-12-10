using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Examples.One;

public static class TestRuleImpl
{
    public static IRuleBase Initialize(ILinguisticBase @base, ComparingMethod method = ComparingMethod.HighestPriority)
    {
        var r1 = FuzzyRule.Create(@base)
            .If("Ret", "Alto")
            .And("Pc", "Bajo")
            .And("Est", "Alto")
            .And("Fz", "Alto")
            .Then("Por", "Alto");
        var r2 = FuzzyRule.Create(@base)
            .If("Ret", "Alto")
            .And("Dpj", "Alto")
            .And("Pc", "Bajo")
            .And("Fz", "Bajo")
            .And("Rea", "Medio")
            .And("Vlsp", "Medio")
            .Then("Def", "Medio");
        var r3 = FuzzyRule.Create(@base)
            .If("Ret", "Alto")
            .And("Dpj", "Alto")
            .And("Pc", "Bajo")
            .And("Fz", "Medio")
            .And("Sal", "Medio")
            .And("Rea", "Alto")
            .Then("Por", "Medio");
        var r4 = FuzzyRule.Create(@base)
            .If("Reg", "Medio")
            .And("Elmp", "Alto")
            .And("Vs", "Bajo")
            .And("Vlsp", "Medio")
            .And("Ag", "Alto")
            .Then("Def", "Medio");
        var r5 = FuzzyRule.Create(@base)
            .If("Cnt", "Bajo")
            .And("Pc", "Bajo")
            .Then("Def", "Bajo");
        var r6 = FuzzyRule.Create(@base)
            .If("Cnt", "Medio")
            .And("Elmp", "Alto")
            .And("Int", "Medio")
            .And("Acel", "Medio")
            .And("Ag", "Medio")
            .And("Sal", "Medio")
            .Then("Med", "Medio");
        var r7 = FuzzyRule.Create(@base)
            .If("Cnt", "Medio")
            .And("Acel", "Medio")
            .And("Elmp", "Alto")
            .And("Int", "Alto")
            .And("Ft", "Medio")
            .And("Cmp", "Medio")
            .And("Bol", "Medio")
            .And("Rea", "Medio")
            .Then("Del", "Medio");
        var r8 = FuzzyRule.Create(@base)
            .If("Elmp", "Medio")
            .And("Ag", "Alto")
            .Then("Del", "Medio");
        var r9 = FuzzyRule.Create(@base)
            .If("Cmp", "Alto")
            .And("Tl", "Medio")
            .And("Elmp", "Bajo")
            .And("Mcje", "Bajo")
            .Then("Del", "Bajo");
        var r10 = FuzzyRule.Create(@base)
            .If("Mcje", "Alto")
            .And("Cnt", "Alto")
            .And("Rea", "Medio")
            .Then("Med", "Medio");
        var r11 = FuzzyRule.Create(@base)
            .If("Def", "Bajo")
            .Then("Hab", "Bajo");
        var r12 = FuzzyRule.Create(@base)
            .If("Def", "Medio")
            .Then("Hab", "Medio");
        var r13 = FuzzyRule.Create(@base)
            .If("Def", "Alto")
            .And("Med", "Alto")
            .Then("Hab", "Alto");
        return RuleBase.Create(method, r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13);
    }
}
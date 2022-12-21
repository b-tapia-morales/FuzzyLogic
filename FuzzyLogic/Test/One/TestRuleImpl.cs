using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;
using static FuzzyLogic.Rule.ComparingMethod;

namespace FuzzyLogic.Test.One;

public class TestRuleImpl : RuleBase
{
    public new static IRuleBase Initialize(ILinguisticBase @base, ComparingMethod method = Priority)
    {
        var r1 = FuzzyRule.Create()
            .If(@base.Is("Ret", "Alto"))
            .And(@base.Is("Pc", "Bajo"))
            .And(@base.Is("Est", "Alto"))
            .And(@base.Is("Fz", "Alto"))
            .Then(@base.Is("Por", "Alto"));
        var r2 = FuzzyRule.Create()
            .If(@base.Is("Ret", "Alto"))
            .And(@base.Is("Dpj", "Alto"))
            .And(@base.Is("Pc", "Bajo"))
            .And(@base.Is("Fz", "Bajo"))
            .And(@base.Is("Rea", "Medio"))
            .And(@base.Is("Vlsp", "Medio"))
            .Then(@base.Is("Def", "Medio"));
        var r3 = FuzzyRule.Create()
            .If(@base.Is("Ret", "Alto"))
            .And(@base.Is("Dpj", "Alto"))
            .And(@base.Is("Pc", "Bajo"))
            .And(@base.Is("Fz", "Medio"))
            .And(@base.Is("Sal", "Medio"))
            .And(@base.Is("Rea", "Alto"))
            .Then(@base.Is("Por", "Medio"));
        var r4 = FuzzyRule.Create()
            .If(@base.Is("Reg", "Medio"))
            .And(@base.Is("Elmp", "Alto"))
            .And(@base.Is("Vs", "Bajo"))
            .And(@base.Is("Vlsp", "Medio"))
            .And(@base.Is("Ag", "Alto"))
            .Then(@base.Is("Def", "Medio"));
        var r5 = FuzzyRule.Create()
            .If(@base.Is("Cnt", "Bajo"))
            .And(@base.Is("Pc", "Bajo"))
            .Then(@base.Is("Def", "Bajo"));
        var r6 = FuzzyRule.Create()
            .If(@base.Is("Cnt", "Medio"))
            .And(@base.Is("Elmp", "Alto"))
            .And(@base.Is("Int", "Medio"))
            .And(@base.Is("Acel", "Medio"))
            .And(@base.Is("Ag", "Medio"))
            .And(@base.Is("Sal", "Medio"))
            .Then(@base.Is("Med", "Medio"));
        var r7 = FuzzyRule.Create()
            .If(@base.Is("Cnt", "Medio"))
            .And(@base.Is("Acel", "Medio"))
            .And(@base.Is("Elmp", "Alto"))
            .And(@base.Is("Int", "Alto"))
            .And(@base.Is("Ft", "Medio"))
            .And(@base.Is("Cmp", "Medio"))
            .And(@base.Is("Bol", "Medio"))
            .And(@base.Is("Rea", "Medio"))
            .Then(@base.Is("Del", "Medio"));
        var r8 = FuzzyRule.Create()
            .If(@base.Is("Elmp", "Medio"))
            .And(@base.Is("Ag", "Alto"))
            .Then(@base.Is("Del", "Medio"));
        var r9 = FuzzyRule.Create()
            .If(@base.Is("Cmp", "Alto"))
            .And(@base.Is("Tl", "Medio"))
            .And(@base.Is("Elmp", "Bajo"))
            .And(@base.Is("Mcje", "Bajo"))
            .Then(@base.Is("Del", "Bajo"));
        var r10 = FuzzyRule.Create()
            .If(@base.Is("Mcje", "Alto"))
            .And(@base.Is("Cnt", "Alto"))
            .And(@base.Is("Rea", "Medio"))
            .Then(@base.Is("Med", "Medio"));
        var r11 = FuzzyRule.Create()
            .If(@base.Is("Def", "Bajo"))
            .Then(@base.Is("Hab", "Bajo"));
        var r12 = FuzzyRule.Create()
            .If(@base.Is("Def", "Medio"))
            .Then(@base.Is("Hab", "Medio"));
        var r13 = FuzzyRule.Create()
            .If(@base.Is("Def", "Alto"))
            .And(@base.Is("Med", "Alto"))
            .Then(@base.Is("Hab", "Alto"));
        return Create(method).AddAll(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13);
    }
}
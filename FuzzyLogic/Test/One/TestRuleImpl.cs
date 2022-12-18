using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;
using static FuzzyLogic.Proposition.FuzzyProposition;
using static FuzzyLogic.Rule.ComparingMethod;

namespace FuzzyLogic.Test.One;

public class TestRuleImpl : RuleBase
{
    public new static IRuleBase Initialize(ILinguisticBase linguisticBase, ComparingMethod method = Priority)
    {
        var r1 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Ret", "Alto"))
            .And(Is(linguisticBase, "Pc", "Bajo"))
            .And(Is(linguisticBase, "Est", "Alto"))
            .And(Is(linguisticBase, "Fz", "Alto"))
            .Then(Is(linguisticBase, "Por", "Alto"));
        var r2 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Ret", "Alto"))
            .And(Is(linguisticBase, "Dpj", "Alto"))
            .And(Is(linguisticBase, "Pc", "Bajo"))
            .And(Is(linguisticBase, "Fz", "Bajo"))
            .And(Is(linguisticBase, "Rea", "Medio"))
            .And(Is(linguisticBase, "Vlsp", "Medio"))
            .Then(Is(linguisticBase, "Def", "Medio"));
        var r3 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Ret", "Alto"))
            .And(Is(linguisticBase, "Dpj", "Alto"))
            .And(Is(linguisticBase, "Pc", "Bajo"))
            .And(Is(linguisticBase, "Fz", "Medio"))
            .And(Is(linguisticBase, "Sal", "Medio"))
            .And(Is(linguisticBase, "Rea", "Alto"))
            .Then(Is(linguisticBase, "Por", "Medio"));
        var r4 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Reg", "Medio"))
            .And(Is(linguisticBase, "Elmp", "Alto"))
            .And(Is(linguisticBase, "Vs", "Bajo"))
            .And(Is(linguisticBase, "Vlsp", "Medio"))
            .And(Is(linguisticBase, "Ag", "Alto"))
            .Then(Is(linguisticBase, "Def", "Medio"));
        var r5 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Cnt", "Bajo"))
            .And(Is(linguisticBase, "Pc", "Bajo"))
            .Then(Is(linguisticBase, "Def", "Bajo"));
        var r6 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Cnt", "Medio"))
            .And(Is(linguisticBase, "Elmp", "Alto"))
            .And(Is(linguisticBase, "Int", "Medio"))
            .And(Is(linguisticBase, "Acel", "Medio"))
            .And(Is(linguisticBase, "Ag", "Medio"))
            .And(Is(linguisticBase, "Sal", "Medio"))
            .Then(Is(linguisticBase, "Med", "Medio"));
        var r7 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Cnt", "Medio"))
            .And(Is(linguisticBase, "Acel", "Medio"))
            .And(Is(linguisticBase, "Elmp", "Alto"))
            .And(Is(linguisticBase, "Int", "Alto"))
            .And(Is(linguisticBase, "Ft", "Medio"))
            .And(Is(linguisticBase, "Cmp", "Medio"))
            .And(Is(linguisticBase, "Bol", "Medio"))
            .And(Is(linguisticBase, "Rea", "Medio"))
            .Then(Is(linguisticBase, "Del", "Medio"));
        var r8 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Elmp", "Medio"))
            .And(Is(linguisticBase, "Ag", "Alto"))
            .Then(Is(linguisticBase, "Del", "Medio"));
        var r9 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Cmp", "Alto"))
            .And(Is(linguisticBase, "Tl", "Medio"))
            .And(Is(linguisticBase, "Elmp", "Bajo"))
            .And(Is(linguisticBase, "Mcje", "Bajo"))
            .Then(Is(linguisticBase, "Del", "Bajo"));
        var r10 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Mcje", "Alto"))
            .And(Is(linguisticBase, "Cnt", "Alto"))
            .And(Is(linguisticBase, "Rea", "Medio"))
            .Then(Is(linguisticBase, "Med", "Medio"));
        var r11 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Def", "Bajo"))
            .Or(Is(linguisticBase, "Med", "Bajo"))
            .Then(Is(linguisticBase, "Hab", "Bajo"));
        var r12 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Def", "Medio"))
            .Or(Is(linguisticBase, "Med", "Medio"))
            .Then(Is(linguisticBase, "Hab", "Medio"));
        var r13 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Def", "Alto"))
            .Or(Is(linguisticBase, "Med", "Alto"))
            .Then(Is(linguisticBase, "Hab", "Alto"));
        return Create(method).AddAll(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13);
    }
}
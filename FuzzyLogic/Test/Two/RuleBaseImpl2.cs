using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;
using static FuzzyLogic.Proposition.FuzzyProposition;
using static FuzzyLogic.Rule.ComparingMethod;

namespace FuzzyLogic.Test.Two;

public class RuleBaseImpl2 : RuleBase
{
    public new static IRuleBase Initialize(ILinguisticBase @base, ComparingMethod method = Priority)
    {
         var r1 = FuzzyRule.Create()
            .If(@base.Is("Horario", "Madrugada"))
            .And(@base.Is("Área", "Pequeña"))
            .Or(@base.Is("Espesor", "Menor"))
            .Then(@base.Is("Tiempo de aplicación", "Corto"));
        var r2 = FuzzyRule.Create()
            .If(@base.Is("Horario", "Madrugada"))
            .And(@base.Is("Área", "Grande"))
            .Or(@base.Is("Espesor", "Menor"))
            .Then(@base.Is("Tiempo de aplicación", "Moderado"));
        var r3 = FuzzyRule.Create()
            .If(@base.Is("Horario", "Día"))
            .And(@base.Is("Área", "Pequeña"))
            .Or(@base.Is("Espesor", "Menor"))
            .Then(@base.Is("Tiempo de aplicación", "Muy Corto"));
        var r4 = FuzzyRule.Create()
            .If(@base.Is("Horario", "Día"))
            .And(@base.Is("Área", "Pequeña"))
            .Or(@base.Is("Espesor", "Regular"))
            .Then(@base.Is("Tiempo de aplicación", "Muy Corto"));
        var r5 = FuzzyRule.Create()
            .If(@base.Is("Horario", "Madrugada"))
            .And(@base.Is("Área", "Pequeña"))
            .And(@base.Is("Espesor", "Menor"))
            .Then(@base.Is("Densidad de Corriente", "Teórica"));
        var r6 = FuzzyRule.Create()
            .If(@base.Is("Horario", "Madrugada"))
            .And(@base.Is("Área", "Grande"))
            .And(@base.Is("Espesor", "Menor"))
            .Then(@base.Is("Densidad de Corriente", "Mínima"));
        var r7 = FuzzyRule.Create()
            .If(@base.Is("Horario", "Día"))
            .And(@base.Is("Área", "Pequeña"))
            .And(@base.Is("Espesor", "Menor"))
            .Then(@base.Is("Densidad de Corriente", "Alta"));
        var r8 = FuzzyRule.Create()
            .If(@base.Is("Horario", "Día"))
            .And(@base.Is("Área", "Pequeña"))
            .And(@base.Is("Espesor", "Regular"))
            .Then(@base.Is("Densidad de Corriente", "Mínima"));
        return Create(method).AddAll(r1, r2, r3, r4, r5, r6, r7, r8);
    }
}
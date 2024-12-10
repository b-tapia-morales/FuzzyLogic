using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Examples.Two;

public class RuleBaseImpl2
{
    public static IRuleBase Initialize(ILinguisticBase @base,
        ComparingMethod method = ComparingMethod.HighestPriority)
    {
        var r1 = FuzzyRule.Create(@base)
            .If("Horario", "Madrugada")
            .And("Área", "Pequeña")
            .Or("Espesor", "Menor")
            .Then("Tiempo de aplicación", "Corto");
        var r2 = FuzzyRule.Create(@base)
            .If("Horario", "Madrugada")
            .And("Área", "Grande")
            .Or("Espesor", "Menor")
            .Then("Tiempo de aplicación", "Moderado");
        var r3 = FuzzyRule.Create(@base)
            .If("Horario", "Día")
            .And("Área", "Pequeña")
            .Or("Espesor", "Menor")
            .Then("Tiempo de aplicación", "Muy Corto");
        var r4 = FuzzyRule.Create(@base)
            .If("Horario", "Día")
            .And("Área", "Pequeña")
            .Or("Espesor", "Regular")
            .Then("Tiempo de aplicación", "Muy Corto");
        var r5 = FuzzyRule.Create(@base)
            .If("Horario", "Madrugada")
            .And("Área", "Pequeña")
            .And("Espesor", "Menor")
            .Then("Densidad de Corriente", "Teórica");
        var r6 = FuzzyRule.Create(@base)
            .If("Horario", "Madrugada")
            .And("Área", "Grande")
            .And("Espesor", "Menor")
            .Then("Densidad de Corriente", "Mínima");
        var r7 = FuzzyRule.Create(@base)
            .If("Horario", "Día")
            .And("Área", "Pequeña")
            .And("Espesor", "Menor")
            .Then("Densidad de Corriente", "Alta");
        var r8 = FuzzyRule.Create(@base)
            .If("Horario", "Día")
            .And("Área", "Pequeña")
            .And("Espesor", "Regular")
            .Then("Densidad de Corriente", "Mínima");
        return RuleBase.Create(method, r1, r2, r3, r4, r5, r6, r7, r8);
    }
}
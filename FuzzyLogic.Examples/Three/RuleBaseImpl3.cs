using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Examples.Three;

public static class RuleBaseImpl3
{
    public static IRuleBase Initialize(ILinguisticBase @base,
        ComparingMethod method = ComparingMethod.HighestPriority)
    {
        var r1 = FuzzyRule.Create(@base)
            .If("Peso", "Medio")
            .And("Azúcar", "Alta")
            .And("Hidratación", "Baja")
            .And("Peso", "Medio")
            .Then("Temperatura", "Media");
        var r2 = FuzzyRule.Create(@base)
            .If("Peso", "Mucho")
            .Or("Hidratación", "Alta")
            .Then("Temperatura", "Alta");
        var r3 = FuzzyRule.Create(@base)
            .If("Grasa", "Alta")
            .Or("Azúcar", "Alta")
            .Then("Temperatura", "Baja");
        var r4 = FuzzyRule.Create(@base)
            .If("Grasa", "Media")
            .And("Azúcar", "Media")
            .And("Peso", "Medio")
            .Then("Temperatura", "Media");
        var r5 = FuzzyRule.Create(@base)
            .If("Temperatura", "Baja")
            .Or("Peso", "Mucho")
            .Then("Tiempo", "Largo");
        var r6 = FuzzyRule.Create(@base)
            .If("Temperatura", "Media")
            .And("Peso", "Medio")
            .Then("Tiempo", "Medio");
        var r7 = FuzzyRule.Create(@base)
            .If("Temperatura", "Baja")
            .And("Peso", "Poco")
            .Then("Tiempo", "Medio");
        var r8 = FuzzyRule.Create(@base)
            .If("Temperatura", "Alta")
            .And("Peso", "Poco")
            .Then("Tiempo", "Corto");

        return RuleBase.Create(method, r1, r2, r3, r4, r5, r6, r7, r8);
    }
}
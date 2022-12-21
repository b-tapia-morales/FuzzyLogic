using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;
using static FuzzyLogic.Rule.ComparingMethod;

namespace FuzzyLogic.Test.Three;

public class RuleBaseImpl3 : RuleBase
{
    public new static IRuleBase Initialize(ILinguisticBase @base, ComparingMethod method = Priority)
    {
        var r1 = FuzzyRule.Create(RulePriority.High)
            .If(@base.Is("Peso", "Medio"))
            .And(@base.Is("Azúcar", "Alta"))
            .And(@base.Is("Hidratación", "Baja"))
            .And(@base.Is("Peso", "Medio"))
            .Then(@base.Is("Temperatura", "Media"));
        var r2 = FuzzyRule.Create()
            .If(@base.Is("Peso", "Mucho"))
            .Or(@base.Is("Hidratación", "Alta"))
            .Then(@base.Is("Temperatura", "Alta"));
        var r3 = FuzzyRule.Create()
            .If(@base.Is("Grasa", "Alta"))
            .Or(@base.Is("Azúcar", "Alta"))
            .Then(@base.Is("Temperatura", "Baja"));
        var r4 = FuzzyRule.Create()
            .If(@base.Is("Grasa", "Media"))
            .And(@base.Is("Azúcar", "Media"))
            .And(@base.Is("Peso", "Medio"))
            .Then(@base.Is("Temperatura", "Media"));
        var r5 = FuzzyRule.Create()
            .If(@base.Is("Temperatura", "Baja"))
            .Or(@base.Is("Peso", "Mucho"))
            .Then(@base.Is("Tiempo", "Largo"));
        var r6 = FuzzyRule.Create(RulePriority.High)
            .If(@base.Is("Temperatura", "Media"))
            .And(@base.Is("Peso", "Medio"))
            .Then(@base.Is("Tiempo", "Medio"));
        var r7 = FuzzyRule.Create()
            .If(@base.Is("Temperatura", "Baja"))
            .And(@base.Is("Peso", "Poco"))
            .Then(@base.Is("Tiempo", "Medio"));
        var r8 = FuzzyRule.Create()
            .If(@base.Is("Temperatura", "Alta"))
            .And(@base.Is("Peso", "Poco"))
            .Then(@base.Is("Tiempo", "Corto"));

        return Create(method).AddAll(r1, r2, r3, r4, r5, r6, r7, r8);
    }
}
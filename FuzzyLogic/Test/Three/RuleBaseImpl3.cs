using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;
using static FuzzyLogic.Proposition.FuzzyProposition;
using static FuzzyLogic.Rule.ComparingMethod;

namespace FuzzyLogic.Test.Three;

public class RuleBaseImpl3 : RuleBase
{
    public new static IRuleBase Initialize(ILinguisticBase linguisticBase, ComparingMethod method = ComparingMethod.HighestPriority)
    {
        var r1 = FuzzyRule.Create(RulePriority.High)
            .If(Is(linguisticBase, "Peso", "Medio"))
            .And(Is(linguisticBase, "Azucar", "Alta"))
            .And(Is(linguisticBase, "Hidratacion", "Baja"))
            .And(Is(linguisticBase, "Peso", "Medio"))
            .Then(Is(linguisticBase, "Temperatura", "Media"));
        var r2 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Peso", "Mucho"))
            .Or(Is(linguisticBase, "Hidratacion", "Alta"))
            .Then(Is(linguisticBase, "Temperatura", "Alta"));
        var r3 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Grasa", "Alta"))
            .Or(Is(linguisticBase, "Azucar", "Alta"))
            .Then(Is(linguisticBase, "Temperatura", "Baja"));
        var r4 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Grasa", "Media"))
            .And(Is(linguisticBase, "Azucar", "Media"))
            .And(Is(linguisticBase, "Peso", "Medio"))
            .Then(Is(linguisticBase, "Temperatura", "Media"));
        var r5 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Temperatura", "Baja"))
            .Or(Is(linguisticBase, "Peso", "Mucho"))
            .Then(Is(linguisticBase, "Tiempo", "Largo"));
        var r6 = FuzzyRule.Create(RulePriority.High)
            .If(Is(linguisticBase, "Temperatura", "Media"))
            .And(Is(linguisticBase, "Peso", "Medio"))
            .Then(Is(linguisticBase, "Tiempo", "Medio"));
        var r7 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Temperatura", "Baja"))
            .And(Is(linguisticBase, "Peso", "Poco"))
            .Then(Is(linguisticBase, "Tiempo", "Medio"));
        var r8 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Temperatura", "Alta"))
            .And(Is(linguisticBase, "Peso", "Poco"))
            .Then(Is(linguisticBase, "Tiempo", "Corto"));
            
        return Create(method).AddAll(r1, r2, r3, r4, r5, r6, r7);

    }
}
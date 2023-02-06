using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Number;
using FuzzyLogic.Rule;
using static FuzzyLogic.Proposition.FuzzyProposition<FuzzyLogic.Number.FuzzyNumber>;

namespace FuzzyLogic.Test.Two;

public class RuleBaseImpl2 : RuleBase<FuzzyNumber>
{
    public new static IRuleBase<FuzzyNumber> Initialize(ILinguisticBase linguisticBase,
        ComparingMethod method = ComparingMethod.HighestPriority)
    {
        var r1 = FuzzyRule<FuzzyNumber>.Create()
            .If(Is(linguisticBase, "Horario", "Madrugada"))
            .And(Is(linguisticBase, "Área", "Pequeña"))
            .Or(Is(linguisticBase, "Espesor", "Menor"))
            .Then(Is(linguisticBase, "Tiempo de aplicación", "Corto"));
        var r2 = FuzzyRule<FuzzyNumber>.Create()
            .If(Is(linguisticBase, "Horario", "Madrugada"))
            .And(Is(linguisticBase, "Área", "Grande"))
            .Or(Is(linguisticBase, "Espesor", "Menor"))
            .Then(Is(linguisticBase, "Tiempo de aplicación", "Moderado"));
        var r3 = FuzzyRule<FuzzyNumber>.Create()
            .If(Is(linguisticBase, "Horario", "Día"))
            .And(Is(linguisticBase, "Área", "Pequeña"))
            .Or(Is(linguisticBase, "Espesor", "Menor"))
            .Then(Is(linguisticBase, "Tiempo de aplicación", "Muy Corto"));
        var r4 = FuzzyRule<FuzzyNumber>.Create()
            .If(Is(linguisticBase, "Horario", "Día"))
            .And(Is(linguisticBase, "Área", "Pequeña"))
            .Or(Is(linguisticBase, "Espesor", "Regular"))
            .Then(Is(linguisticBase, "Tiempo de aplicación", "Muy Corto"));
        var r5 = FuzzyRule<FuzzyNumber>.Create()
            .If(Is(linguisticBase, "Horario", "Madrugada"))
            .And(Is(linguisticBase, "Área", "Pequeña"))
            .And(Is(linguisticBase, "Espesor", "Menor"))
            .Then(Is(linguisticBase, "Densidad de Corriente", "Teórica"));
        var r6 = FuzzyRule<FuzzyNumber>.Create()
            .If(Is(linguisticBase, "Horario", "Madrugada"))
            .And(Is(linguisticBase, "Área", "Grande"))
            .And(Is(linguisticBase, "Espesor", "Menor"))
            .Then(Is(linguisticBase, "Densidad de Corriente", "Mínima"));
        var r7 = FuzzyRule<FuzzyNumber>.Create()
            .If(Is(linguisticBase, "Horario", "Día"))
            .And(Is(linguisticBase, "Área", "Pequeña"))
            .And(Is(linguisticBase, "Espesor", "Menor"))
            .Then(Is(linguisticBase, "Densidad de Corriente", "Alta"));
        var r8 = FuzzyRule<FuzzyNumber>.Create()
            .If(Is(linguisticBase, "Horario", "Día"))
            .And(Is(linguisticBase, "Área", "Pequeña"))
            .And(Is(linguisticBase, "Espesor", "Regular"))
            .Then(Is(linguisticBase, "Densidad de Corriente", "Mínima"));
        return Create(method).AddAll(r1, r2, r3, r4, r5, r6, r7, r8);
    }
}
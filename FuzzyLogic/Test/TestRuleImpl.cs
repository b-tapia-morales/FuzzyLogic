using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Rule;
using static FuzzyLogic.Proposition.FuzzyProposition;

namespace FuzzyLogic.Test;

public class TestRuleImpl : RuleBase
{
    public new static IRuleBase Initialize(ILinguisticBase linguisticBase)
    {
        var r1 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Retencion", "Alto"))
            .And(Is(linguisticBase, "Pase Corto", "Bajo"))
            .And(Is(linguisticBase, "Estirada", "Alto"))
            .And(Is(linguisticBase, "Fuerza", "Alto"))
            .Then(Is(linguisticBase, "Rango Habilidad", "Portero"));
        var r2 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Retencion", "alto"))
            .And(Is(linguisticBase, "Despeje", "Alto"))
            .And(Is(linguisticBase, "Pase Corto", "Bajo"))
            .And(Is(linguisticBase, "Fuerza", "bajo"))
            .And(Is(linguisticBase, "Reaccion", "Medio"))
            .And(Is(linguisticBase, "Velocidad Sprint", "Medio"))
            .Then(Is(linguisticBase, "Rango Habilidad", "Defensa"));
        var r3 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Retencion", "Alto"))
            .And(Is(linguisticBase, "Despeje", "Alto"))
            .And(Is(linguisticBase, "Pase Corto", "Bajo"))
            .And(Is(linguisticBase, "Fuerza", "Bajo"))
            .And(Is(linguisticBase, "Salto", "Medio"))
            .And(Is(linguisticBase, "Reaccion", "Alto"))
            .Then(Is(linguisticBase, "Rango Habilidad", "Portero"));
        var r4 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Regates", "Medio"))
            .And(Is(linguisticBase, "Entrada Limpia", "Alto"))
            .And(Is(linguisticBase, "Vision", "Bajo"))
            .And(Is(linguisticBase, "Velocidad Sprint", "Medio"))
            .And(Is(linguisticBase, "Agresividad", "Alto"))
            .Then(Is(linguisticBase, "Rango Habilidad", "Defensa"));
        var r5 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Control Balon", "Bajo"))
            .And(Is(linguisticBase, "Pase Corto", "bajo"))
            .Then(Is(linguisticBase, "Rango Habilidad", "Defensa"));
        var r6 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Control Balon", "Medio"))
            .And(Is(linguisticBase, "Entrada Limpia", "Alto"))
            .And(Is(linguisticBase, "Intercepcion", "Medio"))
            .And(Is(linguisticBase, "Aceleracion", "Medio"))
            .And(Is(linguisticBase, "Agresividad", "Medio"))
            .And(Is(linguisticBase, "Salto", "Medio"))
            .Then(Is(linguisticBase, "Rango Habilidad", "Medio"));
        var r7 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Control Balon", "Medio"))
            .And(Is(linguisticBase, "Aceleracion", "Bajo"))
            .And(Is(linguisticBase, "Entrada Limpia", "Alto"))
            .And(Is(linguisticBase, "Intercepcion", "ALto"))
            .And(Is(linguisticBase, "Fuerza Tiro", "Medio"))
            .And(Is(linguisticBase, "Compostura", "Medio"))
            .And(Is(linguisticBase, "Boleas", "Medio"))
            .And(Is(linguisticBase, "Reaccion", "Medio"))
            .Then(Is(linguisticBase, "Rango Habilidad", "Delantero"));
        var r8 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Entrada Limpia", "Medio"))
            .And(Is(linguisticBase, "Agresividad", "Alto"))
            .Then(Is(linguisticBase, "Rango Habilidad", "Delantero"));
        var r9 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Compostura", "Alto"))
            .And(Is(linguisticBase, "Precision Tiro Libre", "Medio"))
            .And(Is(linguisticBase, "Entrada Limpia", "Bajo"))
            .And(Is(linguisticBase, "Marcaje", "Bajo"))
            .Then(Is(linguisticBase, "Rango Habilidad", "Delantero"));
        var r10 = FuzzyRule.Create()
            .If(Is(linguisticBase, "Marcaje", "Alto"))
            .And(Is(linguisticBase, "Centro", "Alto"))
            .And(Is(linguisticBase, "Reaccion", "Medio"))
            .Then(Is(linguisticBase, "Rango Habilidad", "Medio"));
        return Create().AddAll(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10);
    }
}
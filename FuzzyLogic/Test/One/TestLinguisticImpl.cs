using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Variable;

namespace FuzzyLogic.Test.One;

public class TestLinguisticImpl : LinguisticBase
{
    public new static ILinguisticBase Initialize()
    {
        var retención = LinguisticVariable
            .Create("Ret")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var paseCorto = LinguisticVariable
            .Create("Pc")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var estirada = LinguisticVariable
            .Create("Est")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var fuerza = LinguisticVariable
            .Create("Fz")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var despeje = LinguisticVariable
            .Create("Dpj")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var reacción = LinguisticVariable
            .Create("Rea")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var velocidadSprint = LinguisticVariable
            .Create("Vlsp")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var salto = LinguisticVariable
            .Create("Sal")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var regates = LinguisticVariable
            .Create("Reg")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var entradaLimpia = LinguisticVariable
            .Create("Elmp")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var visión = LinguisticVariable
            .Create("Vs")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var agresividad = LinguisticVariable
            .Create("Ag")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var controlDelBalón = LinguisticVariable
            .Create("Cb")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var intercepción = LinguisticVariable
            .Create("Int")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var fuerzaDeTiro = LinguisticVariable
            .Create("Ft")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var compostura = LinguisticVariable
            .Create("Cmp")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var boleas = LinguisticVariable
            .Create("Bol")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var precisiónTiroLibre = LinguisticVariable
            .Create("Tl")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var marcaje = LinguisticVariable
            .Create("Mcje")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var centro = LinguisticVariable
            .Create("Cnt")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var aceleración = LinguisticVariable
            .Create("Acel")
            .AddLeftTrapezoidFunction("Bajo", 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddRightTrapezoidFunction("Alto", 0.64, 0.68);
        var portero = LinguisticVariable
            .Create("Por")
            .AddTrapezoidFunction("Bajo", 0.0, 0.0, 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddTrapezoidFunction("Alto", 0.64, 0.68, 1, 1);
        var defensa = LinguisticVariable
            .Create("Def")
            .AddTrapezoidFunction("Bajo", 0.0, 0.0, 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddTrapezoidFunction("Alto", 0.64, 0.68, 1, 1);
        var mediocampista = LinguisticVariable
            .Create("Med")
            .AddTrapezoidFunction("Bajo", 0.0, 0.0, 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddTrapezoidFunction("Alto", 0.64, 0.68, 1, 1);
        var delantero = LinguisticVariable
            .Create("Del")
            .AddTrapezoidFunction("Bajo", 0.0, 0.0, 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddTrapezoidFunction("Alto", 0.64, 0.68, 1, 1);
        var habilidad = LinguisticVariable
            .Create("Hab")
            .AddTrapezoidFunction("Bajo", 0.0, 0.0, 0.31, 0.35)
            .AddTrapezoidFunction("Medio", 0.31, 0.35, 0.64, 0.68)
            .AddTrapezoidFunction("Alto", 0.64, 0.68, 1, 1);
        return Create().AddAll(retención, paseCorto, estirada, fuerza, despeje, reacción, velocidadSprint, salto,
            regates, entradaLimpia, visión, agresividad, controlDelBalón, intercepción, fuerzaDeTiro, compostura,
            boleas, precisiónTiroLibre, marcaje, centro, aceleración, portero, defensa, mediocampista, delantero,
            habilidad);
    }
}
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Variable;

namespace FuzzyLogic.Test;

public class TestLinguisticImpl : LinguisticBase
{
    public new static ILinguisticBase Initialize()
    {
        var retencion = LinguisticVariable
            .Create("Retencion")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var paseCorto = LinguisticVariable
            .Create("Pase Corto")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var estirada = LinguisticVariable
            .Create("Estirada")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var fuerza = LinguisticVariable
            .Create("fuerza")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var despeje = LinguisticVariable
            .Create("Despeje")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var reaccion = LinguisticVariable
            .Create("Reaccion")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var velocidadSprint = LinguisticVariable
            .Create("Velocidad Sprint")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var salto = LinguisticVariable
            .Create("Salto")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var regates = LinguisticVariable
            .Create("Regates")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var entradaLimpia = LinguisticVariable
            .Create("Entrada Limpia")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var vision = LinguisticVariable
            .Create("Vision")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var agresividad = LinguisticVariable
            .Create("Agresividad")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var controlBalon = LinguisticVariable
            .Create("Control balon")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var intercepcion = LinguisticVariable
            .Create("Intercepcion")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var fuerzaTiro = LinguisticVariable
            .Create("Fuerza Tiro")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var compostura = LinguisticVariable
            .Create("Compostura")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var boleas = LinguisticVariable
            .Create("Boleas")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var precisionTiroLibre = LinguisticVariable
            .Create("Precision Tiro Libre")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var marcaje = LinguisticVariable
            .Create("Marcaje")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var centro = LinguisticVariable
            .Create("Centro")
            .AddTriangularFunction("Bajo", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        var rangoHabilidad = LinguisticVariable
            .Create("Rango habilidad")
            .AddTriangularFunction("Portero", 0, 1, 2)
            .AddTriangularFunction("Defensa", 0, 1, 2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Delantero", 0, 1, 2);
        var aceleracion = LinguisticVariable
            .Create("Aceleracion")
            .AddTriangularFunction("Bajo",0,1,2)
            .AddTriangularFunction("Medio", 0, 1, 2)
            .AddTriangularFunction("Alto", 0, 1, 2);
        

        return Create().AddAll(retencion, paseCorto, estirada, fuerza, despeje, reaccion, velocidadSprint, salto,
            regates, entradaLimpia, vision, agresividad, controlBalon, intercepcion, fuerzaTiro, compostura, boleas,
            precisionTiroLibre,marcaje,centro,rangoHabilidad,aceleracion);
    }
}
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Variable;

namespace FuzzyLogic.Examples.Two;

public static class LinguisticBaseImpl2
{
    public static ILinguisticBase Initialize()
    {
        var horario = LinguisticVariable
            .Create("Horario")
            .AddLeftTrapezoidFunction("Madrugada", 3, 8)
            .AddTriangularFunction("Día", 6, 9.5, 13)
            .AddTriangularFunction("Tarde", 12, 16, 20)
            .AddRightTrapezoidFunction("Noche", 12, 24);
        var area = LinguisticVariable
            .Create("Área")
            .AddLeftTrapezoidFunction("Pequeña", 4.5, 120)
            .AddTriangularFunction("Mediana", 40, 125, 210)
            .AddRightTrapezoidFunction("Grande", 130, 278);
        var espesor = LinguisticVariable
            .Create("Espesor")
            .AddLeftTrapezoidFunction("Menor", 0.05, 0.032)
            .AddTriangularFunction("Regular", 0.2, 0.35, 0.5)
            .AddRightTrapezoidFunction("Mayor", 0.38, 0.7);
        var densidadDeCorriente = LinguisticVariable
            .Create("Densidad de Corriente")
            .AddLeftTrapezoidFunction("Mínima", 22.5, 35)
            .AddTriangularFunction("Teórica", 30, 37.5, 45)
            .AddRightTrapezoidFunction("Alta", 40, 52.5);
        var tiempoDeAplicación = LinguisticVariable
            .Create("Tiempo de Aplicación")
            .AddTriangularFunction("Muy corto", 0, 3, 4)
            .AddTriangularFunction("Corto", 3, 5, 7)
            .AddTriangularFunction("Moderado", 6, 11, 16)
            .AddTriangularFunction("Largo", 13, 17, 20);
        return LinguisticBase.Create(horario, area, espesor, densidadDeCorriente, tiempoDeAplicación);
    }
}
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Variable;

namespace FuzzyLogic.Test.Three;

public class LinguisticBaseImpl3 : LinguisticBase
{
    public new static ILinguisticBase Initialize()
    {
        var grasa = LinguisticVariable
            .Create("Grasa")
            .AddTrapezoidFunction("Baja", 0,0,6.5, 15)
            .AddTriangularFunction("Media", 6.5, 14.25, 35)
            .AddRightTrapezoidFunction("Alta", 25, 60);
        var azucar = LinguisticVariable
            .Create("Azucar")
            .AddTrapezoidFunction("Baja", 0,0,5, 10)
            .AddTriangularFunction("Media", 5, 10, 20)
            .AddRightTrapezoidFunction("Alta", 15, 30);
        var hidratacion = LinguisticVariable
            .Create("Hidratacion")
            .AddLeftTrapezoidFunction("Baja", 50, 65)
            .AddTriangularFunction("Media", 50, 65, 73)
            .AddRightTrapezoidFunction("Alta", 70, 82);
        var peso = LinguisticVariable
            .Create("Peso")
            .AddTrapezoidFunction("Poco", 0,0, 50, 100)
            .AddTriangularFunction("Medio", 100, 250, 400)
            .AddTrapezoidFunction("Mucho", 350, 500, 1000, 1000);
        var tiempo = LinguisticVariable
            .Create("Tiempo")
            .AddTrapezoidFunction("Corto", 10,10, 15, 20)
            .AddTriangularFunction("Medio", 20, 30, 40)
            .AddTrapezoidFunction("Largo", 30,40, 60,60);
        var temperatura = LinguisticVariable
            .Create("Temperatura")
            .AddTrapezoidFunction("Baja", 90,90, 120, 160)
            .AddTriangularFunction("Media", 145, 175, 210)
            .AddTrapezoidFunction("Alta", 175, 220, 265, 265);
        return Create().AddAll(grasa, azucar, hidratacion, peso, tiempo, temperatura);

    }
}
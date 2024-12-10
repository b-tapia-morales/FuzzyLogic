using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Variable;

namespace FuzzyLogic.Examples.Four;

public static class LinguisticBaseImpl4
{
    public static ILinguisticBase Create()
    {
        var service = LinguisticVariable
            .Create("quality")
            .AddTriangularFunction("bad", 0, 0, 5)
            .AddTriangularFunction("decent", 0, 5, 10)
            .AddTriangularFunction("great", 5, 10, 10);
        var food = LinguisticVariable
            .Create("service")
            .AddTriangularFunction("poor", 0, 0, 5)
            .AddTriangularFunction("acceptable", 0, 5, 10)
            .AddTriangularFunction("amazing", 5, 10, 10);
        var tip = LinguisticVariable
            .Create("tip")
            .AddTriangularFunction("low", 0, 0, 13)
            .AddTriangularFunction("medium", 0, 13, 25)
            .AddTriangularFunction("high", 13, 25, 35);
        return LinguisticBase.Create(service, food, tip);
    }
}
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Variable;

namespace FuzzyLogic.Test.Four;

public class LinguisticBaseImpl4 : LinguisticBase
{
    public new static ILinguisticBase Initialize()
    {
        var service = LinguisticVariable
            .Create("service")
            .AddTrapezoidFunction("poor", 0, 0, 0, 5)
            .AddTriangularFunction("good", 0, 5, 10)
            .AddTrapezoidFunction("excellent", 5, 10, 10, 10);
        var food = LinguisticVariable
            .Create("food")
            .AddTrapezoidFunction("rancid", 0,0,2,3)
            .AddTrapezoidFunction("delicious", 7,8,10,10);
        var tip = LinguisticVariable
            .Create("tip")
            .AddTriangularFunction("cheap", 0, 5, 10)
            .AddTriangularFunction("average", 10, 15, 20)
            .AddTriangularFunction("generous", 20, 25, 30);
        return Create().AddAll(service, food, tip);

    }
}
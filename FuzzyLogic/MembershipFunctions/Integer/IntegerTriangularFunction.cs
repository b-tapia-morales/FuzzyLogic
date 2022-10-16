using FuzzyLogic.MembershipFunctions.Base;

namespace FuzzyLogic.MembershipFunctions.Integer;

public class IntegerTriangularFunction : BaseTriangularFunction<int>, IIntegerFunction
{
    public IntegerTriangularFunction(string name, int a, int b, int c) : base(name, a, b, c)
    {
    }
}
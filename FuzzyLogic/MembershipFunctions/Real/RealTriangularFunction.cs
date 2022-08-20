namespace FuzzyLogic.MembershipFunctions.Real;

public class RealTriangularFunction : RealTrapezoidalFunction
{
    public RealTriangularFunction(string name, double a, double b, double c) : base(name, a, b, b, c)
    {
    }
}
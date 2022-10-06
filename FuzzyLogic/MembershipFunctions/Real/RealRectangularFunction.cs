namespace FuzzyLogic.MembershipFunctions.Real;

public class RealRectangularFunction : RealTrapezoidalFunction
{
    public RealRectangularFunction(string name, double a, double b) : base(name, a, a, b, b)
    {
    }
    
    public override (double? X1, double? X2) LambdaCutInterval(FuzzyNumber y) => y == 1 ? (A, B) : (null, null);

    public override double? LowerBoundary() => null;

    public override double? UpperBoundary() => null;
}
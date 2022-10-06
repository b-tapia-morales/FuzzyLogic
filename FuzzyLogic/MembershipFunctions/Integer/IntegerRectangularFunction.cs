namespace FuzzyLogic.MembershipFunctions.Integer;

public class IntegerRectangularFunction : IntegerTrapezoidalFunction
{
    public IntegerRectangularFunction(string name, int a, int b) : base(name, a, a, b, b)
    {
    }

    public override (double? X1, double? X2) LambdaCutInterval(FuzzyNumber y) => y == 1 ? (A, B) : (null, null);

    public override int? LowerBoundary() => null;

    public override int? UpperBoundary() => null;
}
namespace FuzzyLogic.MembershipFunctions.Integer;

public class IntegerRectangularFunction : IntegerTrapezoidalFunction
{
    public IntegerRectangularFunction(string name, int a, int b) : base(name, a, a, b, b)
    {
    }

    public override int? LowerBoundary() => null;
    
    public override int? UpperBoundary() => null;
}
using FuzzyLogic.MembershipFunctions.Base;

namespace FuzzyLogic.MembershipFunctions.Real;

public class RealCauchyFunction: BaseCauchyFunction<double>, IRealFunction
{
    public RealCauchyFunction(string name, double a, double b, double c) : base(name, a, b, c)
    {
        A = a;
        B = b;
        C = c;
    }
    
    protected override double A { get; }
    protected override double B { get; }
    protected override double C { get; }
}
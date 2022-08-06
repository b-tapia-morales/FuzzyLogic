namespace FuzzyLogic.MembershipFunction.Real;

public class SigmoidFunction : IRealFunction
{
    public SigmoidFunction(string name, double a, double c)
    {
        Name = name;
        A = a;
        C = c;
    }

    public string Name { get; }
    public double A { get; }
    public double C { get; }

    public FuzzyNumber MembershipDegree(double x) => 1 / (1 + Math.Pow(Math.E, -A * (x - C)));
}
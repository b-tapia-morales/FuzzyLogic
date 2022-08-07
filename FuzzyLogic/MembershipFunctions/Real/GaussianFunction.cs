namespace FuzzyLogic.MembershipFunctions.Real;

public class GaussianFunction : IMembershipFunction<double>
{
    public GaussianFunction(string name, double m, double o)
    {
        Name = name;
        M = m;
        O = o;
    }

    public string Name { get; }
    public double M { get; }
    public double O { get; }

    public FuzzyNumber MembershipDegree(double x) => Math.Min(1.0, Math.Pow(Math.E, -0.5 * Math.Pow((x - M) / O, 2)));
}
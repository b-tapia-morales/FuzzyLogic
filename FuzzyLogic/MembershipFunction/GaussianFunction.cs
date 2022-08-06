namespace FuzzyLogic.MembershipFunction;

public class GaussianFunction : IMembershipFunction
{
    public GaussianFunction(string name, double m, double o, double minValue, double maxValue)
    {
        Name = name;
        M = m;
        O = o;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public string Name { get; }
    public double M { get; }
    public double O { get; }
    public double MinValue { get; }
    public double MaxValue { get; }

    public FuzzyNumber MembershipDegree(double x)
    {
        return Math.Min(1.0, Math.Pow(Math.E, -0.5 * Math.Pow((x - M) / O, 2)));
    }
}
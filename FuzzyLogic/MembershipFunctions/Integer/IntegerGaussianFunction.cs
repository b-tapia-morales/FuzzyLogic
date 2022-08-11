namespace FuzzyLogic.MembershipFunctions.Integer;

public class IntegerGaussianFunction : BaseGaussianFunction<int>
{
    public IntegerGaussianFunction(string name, int m, int o) : base(name, m, o)
    {
        M = m;
        O = o;
    }

    protected override int M { get; }
    protected override int O { get; }
}
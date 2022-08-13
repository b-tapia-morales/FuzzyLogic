using FuzzyLogic.MembershipFunctions.Base;

namespace FuzzyLogic.MembershipFunctions.Integer;

public class IntegerSigmoidFunction : BaseSigmoidFunction<int>, IIntegerFunction
{
    public IntegerSigmoidFunction(string name, int a, int c) : base(name, a, c)
    {
        A = a;
        C = c;
    }

    protected override int A { get; }
    protected override int C { get; }
}
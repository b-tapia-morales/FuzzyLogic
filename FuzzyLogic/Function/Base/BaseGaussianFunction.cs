using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using static System.Math;
using static FuzzyLogic.Function.Interface.IMembershipFunction<double>;

namespace FuzzyLogic.Function.Base;

public class BaseGaussianFunction : BaseMembershipFunction, IAsymptoteFunction<double>
{
    public double Inflection { get; }

    protected BaseGaussianFunction(string name, double mu, double sigma, double uMax = 1) : base(name, uMax)
    {
        CheckSigma(sigma);
        Mu = mu;
        Sigma = Inflection = sigma;
    }

    protected double Mu { get; }
    protected double Sigma { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => true;

    public override bool IsSingleton() => false;

    public override double SupportLeft() => double.NegativeInfinity;

    public override double SupportRight() => double.PositiveInfinity;

    public override double? CoreLeft() => Abs(1 - UMax) <= FuzzyNumber.Epsilon ? Mu : null;

    public override double? CoreRight() => Abs(1 - UMax) <= FuzzyNumber.Epsilon ? Mu : null;

    public override double? AlphaCutLeft(FuzzyNumber cut)
    {
        if (cut.Value > UMax)
            return null;
        if (Abs(cut.Value - UMax) <= FuzzyNumber.Epsilon)
            return Mu;
        return Mu - Sigma * Sqrt(2 * Log(1 / cut.Value));
    }

    public override double? AlphaCutRight(FuzzyNumber cut)
    {
        if (cut.Value > UMax)
            return null;
        if (Abs(cut.Value - UMax) <= FuzzyNumber.Epsilon)
            return Mu;
        return Mu + Sigma * Sqrt(2 * Log(1 / cut.Value));
    }

    public override Func<double, double> LarsenProduct(FuzzyNumber lambda) =>
        x => lambda.Value * Exp(-(1 / 2.0) * Pow((x - Mu) / Sigma, 2));

    public bool IsMonotonicallyIncreasing() => false;

    public bool IsMonotonicallyDecreasing() => false;

    public bool IsUnimodal() => true;

    public double ApproxSupportLeft() => Mu - 3 * Sigma;

    public double ApproxSupportRight() => Mu + 3 * Sigma;

    public double? ApproxCoreLeft() => CoreLeft();

    public double? ApproxCoreRight() => CoreRight();

    private static void CheckSigma(double sigma)
    {
        if (Abs(sigma) < DeltaX)
            throw new ArgumentException("The value for «o» cannot be equal to 0");
    }
}
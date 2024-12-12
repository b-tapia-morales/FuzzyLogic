using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using static System.Math;

// ReSharper disable MemberCanBePrivate.Global

namespace FuzzyLogic.Function.Real;

public class GaussianFunction : AsymptoteFunction
{
    public GaussianFunction(string name, double mu, double sigma, double uMax = 1) : base(name, uMax)
    {
        CheckSigma(sigma);
        Mu = mu;
        Sigma = Inflection = sigma;
    }

    public override double Inflection { get; }

    public double Mu { get; }
    public double Sigma { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => true;

    public override bool IsPrototypical() => false;

    public override double? PeakLeft() => Mu;

    public override double? PeakRight() => Mu;

    public override double? CoreLeft() => Abs(1 - UMax) <= FuzzyNumber.Epsilon ? Mu : null;

    public override double? CoreRight() => Abs(1 - UMax) <= FuzzyNumber.Epsilon ? Mu : null;

    public override double? AlphaCutLeft(FuzzyNumber alpha)
    {
        if (alpha.Value > UMax)
            return null;
        if (Abs(alpha.Value - UMax) <= FuzzyNumber.Epsilon)
            return Mu;
        return Mu - Sigma * Sqrt(2 * Log(1 / alpha.Value));
    }

    public override double? AlphaCutRight(FuzzyNumber alpha)
    {
        if (alpha.Value > UMax)
            return null;
        if (Abs(alpha.Value - UMax) <= FuzzyNumber.Epsilon)
            return Mu;
        return Mu + Sigma * Sqrt(2 * Log(1 / alpha.Value));
    }

    public override Func<double, double> LarsenProduct(FuzzyNumber lambda) =>
        x => lambda.Value * Exp(-(1 / 2.0) * Pow((x - Mu) / Sigma, 2));

    public override IMembershipFunction DeepCopy() => new GaussianFunction(Name, Mu, Sigma, UMax);

    public override IMembershipFunction DeepCopyRenamed(string name) => new GaussianFunction(name, Mu, Sigma, UMax);

    public override bool IsMonotonicallyIncreasing() => false;

    public override bool IsMonotonicallyDecreasing() => false;

    public override bool IsUnimodal() => true;

    public override double ApproxSupportLeft() => Mu - 4 * Sigma;

    public override double ApproxSupportRight() => Mu + 4 * Sigma;

    public override double? ApproxCoreLeft() => CoreLeft();

    public override double? ApproxCoreRight() => CoreRight();

    public override string ToString() => $"Linguistic term: {Name} - Membership Function: Gaussian - Sides: (μ: {Mu}, σ: {Sigma}) - μMax: {UMax}";

    private static void CheckSigma(double sigma)
    {
        if (Abs(sigma) <= IMembershipFunction.DeltaX)
            throw new ArgumentException("The value for «o» cannot be equal to 0");
    }
}
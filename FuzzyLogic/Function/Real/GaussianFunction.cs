﻿using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Function.Real;

public class GaussianFunction : AsymptoteFunction
{
    public override double Inflection { get; }

    internal protected GaussianFunction(string name, double mu, double sigma, double uMax = 1) : base(name, uMax)
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

    public override bool IsMonotonicallyIncreasing() => false;

    public override bool IsMonotonicallyDecreasing() => false;

    public override bool IsUnimodal() => true;

    public override double ApproxSupportLeft() => Mu - 3 * Sigma;

    public override double ApproxSupportRight() => Mu + 3 * Sigma;

    public override double? ApproxCoreLeft() => CoreLeft();

    public override double? ApproxCoreRight() => CoreRight();

    private static void CheckSigma(double sigma)
    {
        if (Abs(sigma) <= IMembershipFunction.DeltaX)
            throw new ArgumentException("The value for «o» cannot be equal to 0");
    }
}
﻿using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Real;

public abstract class MembershipFunction : IMembershipFunction
{
    public string Name { get; }
    public double UMax { get; }

    protected MembershipFunction(string name, double uMax)
    {
        CheckName(name);
        CheckHeight(uMax);
        Name = name;
        UMax = uMax;
    }

    public abstract bool IsOpenLeft();

    public abstract bool IsOpenRight();

    public abstract bool IsSymmetric();

    public abstract bool IsSingleton();

    public abstract double? PeakLeft();

    public abstract double? PeakRight();

    public abstract double SupportLeft();

    public abstract double SupportRight();

    public abstract double? CoreLeft();

    public abstract double? CoreRight();

    public abstract double? AlphaCutLeft(FuzzyNumber cut);

    public abstract double? AlphaCutRight(FuzzyNumber cut);

    public abstract Func<double, double> LarsenProduct(FuzzyNumber lambda);

    public virtual double FiniteSupportLeft() => SupportLeft();

    public virtual double FiniteSupportRight() => SupportRight();

    private static void CheckHeight(double h)
    {
        if (Math.Abs(h) <= FuzzyNumber.Epsilon || h > 1)
            throw new ArgumentException(
                $"The height “h” of the function must be in the range [0, 1] (Provided value was: {h})");
    }

    private static void CheckName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "The string corresponding to the name of the function cannot be null or contain only whitespaces");
    }
}
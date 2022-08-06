﻿namespace FuzzyLogic.MembershipFunction;

public class TrapezoidFunction : IMembershipFunction
{
    public TrapezoidFunction(string name, double a, double b, double c, double d)
    {
        Name = name;
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public string Name { get; }
    public double A { get; }
    public double B { get; }
    public double C { get; }
    public double D { get; }

    public double? LowerBoundary() => A;

    public double? UpperBoundary() => B;

    public FuzzyNumber MembershipDegree(double x)
    {
        if (x <= A) return 0.0;
        if (x >= A && x <= B) return (x - A) / (B - A);
        if (x >= B && x <= C) return 1.0;
        if (x >= C && x <= D) return (D - x) / (D - C);
        return 0.0;
    }
}
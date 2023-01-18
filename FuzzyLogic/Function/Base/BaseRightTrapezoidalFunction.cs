﻿using System.Numerics;
using FuzzyLogic.Function.Interface;

namespace FuzzyLogic.Function.Base;

public abstract class BaseRightTrapezoidalFunction<T> : BaseMembershipFunction<T>, ITrapezoidalFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    protected BaseRightTrapezoidalFunction(string name, T a, T b, T h) : base(name)
    {
        A = a;
        B = b;
        H = h;
    }

    protected BaseRightTrapezoidalFunction(string name, T a, T b) : this(name, a, b, T.One)
    {
    }

    protected T A { get; }
    protected T B { get; }

    public static Func<T, double> AsFunction(double a, double b, double h) => t =>
    {
        var x = t.ToDouble(null);
        if (x > a && x < b)
            return h * ((x - a) / (b - a));
        if (x >= b)
            return h;
        return 0;
    };

    public static Func<T, double> AsFunction(double a, double b) => AsFunction(a, b, 1);

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => false;

    public override Func<T, double> AsFunction() =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), H.ToDouble(null));

    public abstract (T? X0, T? X1) CoreInterval();
}
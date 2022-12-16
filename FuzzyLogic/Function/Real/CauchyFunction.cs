﻿using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;

namespace FuzzyLogic.Function.Real;

public class CauchyFunction: BaseCauchyFunction<double>, IRealFunction
{
    public CauchyFunction(string name, double a, double b, double c) : base(name, a, b, c)
    {
    }

    public override double LowerBoundary() => double.NegativeInfinity;

    public override double UpperBoundary() => double.PositiveInfinity;
}
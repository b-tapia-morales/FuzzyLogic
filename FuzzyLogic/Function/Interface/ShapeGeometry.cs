using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Function.Real;
using FuzzyLogic.Number;
using FuzzyLogic.Utils;
using MathNet.Numerics.Integration;

namespace FuzzyLogic.Function.Interface;

public static class ShapeGeometry
{
    public const double ErrorMargin = 1e-4;

    public static double CalculateArea(this IMembershipFunction function, FuzzyNumber weight,
        ImplicationMethod method, double errorMargin = ErrorMargin)
    {
        if (weight == 0)
            return 0;
        var (x0, x1) = function.FiniteSupportInterval();
        return function.CalculateArea(weight, x0, x1, method, errorMargin);
    }

    public static double CalculateArea(this IMembershipFunction function, FuzzyNumber weight, double x0, double x1,
        ImplicationMethod method, double errorMargin = ErrorMargin)
    {
        if (weight == 0)
            return 0;
        switch (function)
        {
            case TriangularFunction triangle when triangle.A >= x0 && triangle.C <= x1:
            case TrapezoidalFunction trapezoid when trapezoid.A >= x0 && trapezoid.D <= x1:
                return function.CalculatePolygonArea(weight, method);
            default:
            {
                var pureFunction = function.RetrieveFunction(weight, method);
                return Integrate(pureFunction, x0, x1, errorMargin);
            }
        }
    }

    public static double? CentroidXCoordinate(this IMembershipFunction function, FuzzyNumber weight, double x0, double x1,
        ImplicationMethod method, double errorMargin = ErrorMargin)
    {
        if (weight == 0)
            return null;
        switch (function)
        {
            case TriangularFunction triangle when triangle.A >= x0 && triangle.C <= x1:
                return TrigonometricUtils.TriangleXCentroid(triangle.A, triangle.B, triangle.C);
            case TrapezoidalFunction trapezoid when trapezoid.A >= x0 && trapezoid.D <= x1:
                return TrigonometricUtils.TrapezoidXCentroid(trapezoid.D - trapezoid.A, trapezoid.C - trapezoid.B, trapezoid.UMax);
            default:
            {
                var pureFunction = function.RetrieveFunction(weight, method);
                var area = function.CalculateArea(weight, x0, x1, method, errorMargin);
                return 1 / area * Integrate(pureFunction, x0, x1, errorMargin);
            }
        }

    }

    public static double? CentroidXCoordinate(this IMembershipFunction function, FuzzyNumber weight,
        ImplicationMethod method, double errorMargin = ErrorMargin)
    {
        if (weight == 0)
            return null;
        var (x0, x1) = function.FiniteSupportInterval();
        return function.CentroidXCoordinate(weight, x0, x1, method, errorMargin);
    }

    public static double? CentroidYCoordinate(this IMembershipFunction function, FuzzyNumber weight, double x0, double x1,
        ImplicationMethod method, double errorMargin = ErrorMargin)
    {
        if (weight == 0)
            return null;
        switch (function)
        {
            case TriangularFunction triangle when triangle.A >= x0 && triangle.C <= x1:
                return TrigonometricUtils.TriangleYCentroid(triangle.UMax);
            case TrapezoidalFunction trapezoid when trapezoid.A >= x0 && trapezoid.D <= x1:
                return TrigonometricUtils.TrapezoidYCentroid(trapezoid.UMax);
            default:
            {
                var pureFunction = function.RetrieveFunction(weight, method);
                var area = function.CalculateArea(weight, x0, x1, method, errorMargin);
                return 1 / (2 * area) * Integrate(pureFunction, x0, x1, errorMargin);
            }
        }
    }

    public static double? CentroidYCoordinate(this IMembershipFunction function, FuzzyNumber weight,
        ImplicationMethod method, double errorMargin = ErrorMargin)
    {
        if (weight == 0)
            return null;
        var (x0, x1) = function.FiniteSupportInterval();
        return function.CentroidYCoordinate(weight, x0, x1, method, errorMargin);
    }

    public static (double? X, double? Y) CalculateCentroid(this IMembershipFunction function, FuzzyNumber weight, double x0, double x1,
        ImplicationMethod method, double errorMargin = ErrorMargin) =>
        (function.CentroidXCoordinate(weight, x0, x1, method, errorMargin),
            function.CentroidYCoordinate(weight, x0, x1, method, errorMargin));

    public static (double? X, double? Y) CalculateCentroid(this IMembershipFunction function, FuzzyNumber weight,
        ImplicationMethod method, double errorMargin = ErrorMargin) =>
        (function.CentroidXCoordinate(weight, method, errorMargin), function.CentroidYCoordinate(weight, method, errorMargin));

    private static double CalculatePolygonArea(this IMembershipFunction function, FuzzyNumber weight, ImplicationMethod method)
    {
        switch (function)
        {
            case TriangularFunction triangle when weight == 1 || method == ImplicationMethod.Larsen:
                return TrigonometricUtils.TriangleArea(triangle.C - triangle.B, triangle.UMax);
            case TrapezoidalFunction trapezoid when weight == 1 || method == ImplicationMethod.Larsen:
                return TrigonometricUtils.TrapezoidArea(trapezoid.D - trapezoid.A, trapezoid.C - trapezoid.B, trapezoid.UMax);
        }

        var (x0, x1) = function.FiniteSupportInterval();
        var (a0, a1) = function.AlphaCutInterval(weight);
        return TrigonometricUtils.TrapezoidArea(x0, a0!.Value, a1!.Value, x1, function.UMax);
    }

    private static Func<double, double> RetrieveFunction(this IMembershipFunction function, FuzzyNumber y, ImplicationMethod method) => method switch
    {
        ImplicationMethod.Mamdani => function.MamdaniMinimum(y),
        ImplicationMethod.Larsen => function.LarsenProduct(y),
        _ => throw new NotImplementedException()
    };

    private static double Integrate(Func<double, double> function, double x0, double x1,
        double errorMargin = ErrorMargin) =>
        NewtonCotesTrapeziumRule.IntegrateAdaptive(function, x0, x1, errorMargin);
}
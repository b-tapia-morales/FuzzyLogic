using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Implication;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Utils;
using static System.Math;

namespace FuzzyLogic.Function.Real;

public class TriangularFunction : BaseTriangularFunction<double>, IFuzzyInference
{
    public TriangularFunction(string name, double a, double b, double c, double h = 1) : base(name, a, b, c, h)
    {
    }

    public override double MaxHeightLeftEndpoint() => B;

    public override double MaxHeightRightEndpoint() => B;

    public double CalculateArea(double errorMargin = IClosedShape.DefaultErrorMargin) =>
        TrigonometricUtils.TriangleArea(Abs(A - C), H);

    public double CentroidXCoordinate(double errorMargin = IClosedShape.DefaultErrorMargin) =>
        CalculateCentroid(errorMargin).X;

    public double CentroidYCoordinate(double errorMargin = IClosedShape.DefaultErrorMargin) =>
        CalculateCentroid(errorMargin).Y;

    public (double X, double Y) CalculateCentroid(double errorMargin = IClosedShape.DefaultErrorMargin) =>
        TrigonometricUtils.TriangleCentroid(A, B, C, H);

    public double? MamdaniCutLeftEndpoint<T>(T y) where T : struct, IFuzzyNumber<T> =>
        A + (y / H) * (B - A);

    public double? MamdaniCutRightEndpoint<T>(T y) where T : struct, IFuzzyNumber<T> =>
        C - (y / H) * (C - B);

    public double MamdaniCutArea<T>(T y, double errorMargin = IClosedShape.DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T>
    {
        if (y == 0)
            throw new ArgumentException("Can't calculate the area of the zero-function");
        if (y >= H)
            return (this as IClosedShape).CalculateArea(errorMargin);
        var (x1, x2) = (this as IMamdaniMinimum).LambdaCutInterval(y).GetValueOrDefault();
        return TrigonometricUtils.TrapezoidArea(Abs(x1 - x2), Abs(A - C), y);
    }

    public double MamdaniCentroidXCoordinate<T>(T y, double errorMargin = IClosedShape.DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T> => MamdaniCutCentroid(y, errorMargin).X;

    public double MamdaniCentroidYCoordinate<T>(T y, double errorMargin = IClosedShape.DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T> => MamdaniCutCentroid(y, errorMargin).Y;

    public (double X, double Y) MamdaniCutCentroid<T>(T y, double errorMargin = IClosedShape.DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T>
    {
        if (y == 0)
            throw new ArgumentException("Can't calculate the area of the zero-function");
        if (Abs(y - 1) < IFuzzyNumber<T>.Tolerance)
            return CalculateCentroid(errorMargin);
        var (x1, x2) = (this as IMamdaniMinimum).LambdaCutInterval(y).GetValueOrDefault();
        return TrigonometricUtils.TrapezoidCentroid(A, x1, x2, C, Min(H, y));
    }
}
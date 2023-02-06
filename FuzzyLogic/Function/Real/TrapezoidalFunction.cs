using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Implication;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Utils;
using static System.Math;

namespace FuzzyLogic.Function.Real;

public class TrapezoidalFunction : BaseTrapezoidalFunction<double>, IFuzzyInference
{
    public TrapezoidalFunction(string name, double a, double b, double c, double d, double h) :
        base(name, a, b, c, d, h)
    {
    }

    public TrapezoidalFunction(string name, double a, double b, double c, double d) : base(name, a, b, c, d)
    {
    }

    public double CalculateArea(double errorMargin = IClosedShape.DefaultErrorMargin) =>
        TrigonometricUtils.TrapezoidArea(Abs(B - C), Abs(A - D), H);

    public double CentroidXCoordinate(double errorMargin = IClosedShape.DefaultErrorMargin) =>
        CalculateCentroid(errorMargin).X;

    public double CentroidYCoordinate(double errorMargin = IClosedShape.DefaultErrorMargin) =>
        CalculateCentroid(errorMargin).Y;

    public (double X, double Y) CalculateCentroid(double errorMargin = IClosedShape.DefaultErrorMargin) =>
        TrigonometricUtils.TrapezoidCentroid(A, B, C, D, H);

    public double? MamdaniCutLeftEndpoint<T>(T y) where T : struct, IFuzzyNumber<T> =>
        y > H ? null : A + (Min(y, H) / H) * (B - A);

    public double? MamdaniCutRightEndpoint<T>(T y) where T : struct, IFuzzyNumber<T> =>
        y > H ? null : D - (Min(y, H) / H) * (D - C);

    public double MamdaniCutArea<T>(T y, double errorMargin = IClosedShape.DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T>
    {
        if (y == 0)
            throw new ArgumentException("Can't calculate the area of the zero-function");
        if (y >= H)
            return CalculateArea(errorMargin);
        var (x1, x2) = (this as IMamdaniMinimum).LambdaCutInterval<T>(Min(y, T.Of(H))).GetValueOrDefault();
        return TrigonometricUtils.TrapezoidArea(Abs(x1 - x2), Abs(A - D), y);
    }

    public double MamdaniCentroidXCoordinate<T>(T y, double errorMargin = IClosedShape.DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T> => TrigonometricUtils.TrapezoidCentroid(A, B, C, D, Min(y, H)).X;

    public double MamdaniCentroidYCoordinate<T>(T y, double errorMargin = IClosedShape.DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T> => TrigonometricUtils.TrapezoidCentroid(A, B, C, D, Min(y, H)).Y;

    public (double X, double Y) MamdaniCutCentroid<T>(T y, double errorMargin = IClosedShape.DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T>
    {
        if (y == 0)
            throw new ArgumentException("Can't calculate the area of the zero-function");
        if (y >= H)
            return CalculateCentroid(errorMargin);
        var (x1, x2) = (this as IMamdaniMinimum).LambdaCutInterval<T>(Min(y, H)).GetValueOrDefault();
        return TrigonometricUtils.TrapezoidCentroid(A, x1, x2, D, Min(y, H));
    }

    public override double MaxHeightLeftEndpoint() => B;

    public override double MaxHeightRightEndpoint() => C;
}
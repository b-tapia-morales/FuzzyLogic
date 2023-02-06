using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Implication;

public interface ILarsenProduct : IFuzzyImplication
{
    Func<double, double> IFuzzyImplication.LambdaCutFunction<T>(T y) =>
        LarsenCutFunction(y);

    double? IFuzzyImplication.LambdaCutLeftEndpoint<T>(T y) =>
        MaxHeightLeftEndpoint();

    double? IFuzzyImplication.LambdaCutRightEndpoint<T>(T y) =>
        MaxHeightRightEndpoint();

    (double X1, double X2)? IFuzzyImplication.LambdaCutInterval<T>(T y) =>
        MaxHeightInterval();

    double IFuzzyImplication.CalculateArea<T>(T y, double errorMargin) =>
        LarsenCutArea(y, errorMargin);

    double IFuzzyImplication.CentroidXCoordinate<T>(T y, double errorMargin) =>
        LarsenCentroidXCoordinate(y, errorMargin);

    double IFuzzyImplication.CentroidYCoordinate<T>(T y, double errorMargin) =>
        LarsenCentroidYCoordinate(y, errorMargin);

    (double X, double Y) IFuzzyImplication.CalculateCentroid<T>(T y, double errorMargin) =>
        LarsenCutCentroid(y, errorMargin);

    protected Func<double, double> LarsenCutFunction<T>(T y) where T : struct, IFuzzyNumber<T> =>
        HeightFunction(y);

    protected double LarsenCutArea<T>(T y, double errorMargin) where T : struct, IFuzzyNumber<T>
    {
        if (y == 0.0)
            throw new ArgumentException("Can't calculate the area of the zero-function");
        if (y >= H)
            return (this as IClosedShape).CalculateArea(errorMargin);
        var (x1, x2) = ClosedInterval();
        return Integrate(HeightFunction(y), x1, x2, errorMargin);
    }

    protected double LarsenCentroidXCoordinate<T>(T y, double errorMargin = DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T> => CentroidXCoordinate(this, y, errorMargin);

    protected double LarsenCentroidYCoordinate<T>(T y, double errorMargin = DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T> => CentroidYCoordinate(this, y, errorMargin);

    protected (double X, double Y) LarsenCutCentroid<T>(T y, double errorMargin = DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T> =>
        (LarsenCentroidXCoordinate(y, errorMargin), LarsenCentroidYCoordinate(y, errorMargin));
}
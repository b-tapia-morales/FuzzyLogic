using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Function.Implication;

public interface IMamdaniMinimum : IFuzzyImplication
{
    Func<double, double> IFuzzyImplication.LambdaCutFunction<T>(T y) =>
        MamdaniCutFunction(y);

    double IFuzzyImplication.CalculateArea<T>(T y, double errorMargin) =>
        MamdaniCutArea(y, errorMargin);

    double IFuzzyImplication.CentroidXCoordinate<T>(T y, double errorMargin) =>
        MamdaniCentroidXCoordinate(y, errorMargin);

    double IFuzzyImplication.CentroidYCoordinate<T>(T y, double errorMargin) =>
        MamdaniCentroidYCoordinate(y, errorMargin);

    (double X, double Y) IFuzzyImplication.CalculateCentroid<T>(T y, double errorMargin) =>
        MamdaniCutCentroid(y, errorMargin);

    protected double? MamdaniCutLeftEndpoint<T>(T y) where T : struct, IFuzzyNumber<T>;

    protected double? MamdaniCutRightEndpoint<T>(T y) where T : struct, IFuzzyNumber<T>;

    protected (double X1, double X2)? MamdaniCutInterval<T>(T y) where T : struct, IFuzzyNumber<T> =>
        y >= H ? null : (MamdaniCutLeftEndpoint(y).GetValueOrDefault(), MamdaniCutRightEndpoint(y).GetValueOrDefault());

    protected Func<double, double> MamdaniCutFunction<T>(T y) where T : struct, IFuzzyNumber<T> => x =>
    {
        if (y == 0) return 0.0;
        if (y >= H) return AsFunction().Invoke(x);
        var (leftEndpoint, rightEndpoint) = ClosedInterval();
        if (x < leftEndpoint || x > rightEndpoint) return 0.0;
        var (leftCut, rightCut) = LambdaCutInterval(y).GetValueOrDefault();
        return x < leftCut || x > rightCut ? AsFunction().Invoke(x) : y;
    };

    protected double MamdaniCutArea<T>(T y, double errorMargin = DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T>
    {
        if (y == 0)
            throw new ArgumentException("Can't calculate the area of the zero-function");
        if (y >= H)
            return (this as IClosedShape).CalculateArea(errorMargin);
        var (x1, x2) = ClosedInterval();
        var (l1, l2) = LambdaCutInterval(y).GetValueOrDefault();
        var rectangleArea = Abs(l1 - l2) * y;
        var leftArea = Integrate(AsFunction(), x1, l1, errorMargin);
        var rightArea = Integrate(AsFunction(), l2, x2, errorMargin);
        return leftArea + rectangleArea + rightArea;
    }

    protected double MamdaniCentroidXCoordinate<T>(T y, double errorMargin = DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T> => CentroidXCoordinate(this, y, errorMargin);

    protected double MamdaniCentroidYCoordinate<T>(T y, double errorMargin = DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T> => CentroidYCoordinate(this, y, errorMargin);

    protected (double X, double Y) MamdaniCutCentroid<T>(T y, double errorMargin = DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T> =>
        (MamdaniCentroidXCoordinate(y, errorMargin), MamdaniCentroidYCoordinate(y, errorMargin));
}
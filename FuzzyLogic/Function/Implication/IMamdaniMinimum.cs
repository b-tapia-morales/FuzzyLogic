using System.Numerics;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Implication;

public interface IMamdaniMinimum<T> : IFuzzyImplication<T> where T : unmanaged, INumber<T>, IConvertible
{
    Func<T, double> LambdaCutFunction(FuzzyNumber y) => x =>
    {
        double cutPoint = y.Value, height = H.ToDouble(null);
        if (cutPoint == 0) return 0.0;
        if (cutPoint >= height) return AsFunction().Invoke(x);
        var (leftEndpoint, rightEndpoint) = ClosedInterval();
        if (x < leftEndpoint || x > rightEndpoint) return 0.0;
        var (leftCut, rightCut) = LambdaCutInterval(y);
        return x.ToDouble(null) < leftCut || x.ToDouble(null) > rightCut ? AsFunction().Invoke(x) : y;
    };

    Func<T, double> LambdaCutFunction(FuzzyNumber y, double x0, double x1) => x =>
        (x.ToDouble(null) < x0 || x.ToDouble(null) > x1) ? 0.0 : LambdaCutFunction(y).Invoke(x);

    (double X1, double X2) LambdaCutInterval(FuzzyNumber y) => (LambdaCutLeftEndpoint(y), LambdaCutRightEndpoint(y));

    double LambdaCutLeftEndpoint(FuzzyNumber y);

    double LambdaCutRightEndpoint(FuzzyNumber y);
}
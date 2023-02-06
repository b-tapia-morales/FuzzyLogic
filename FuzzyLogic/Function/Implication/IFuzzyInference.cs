using FuzzyLogic.Number;
using static FuzzyLogic.Function.Implication.InferenceMethod;

namespace FuzzyLogic.Function.Implication;

public interface IFuzzyInference : IMamdaniMinimum, ILarsenProduct
{
    Func<double, double> IFuzzyImplication.LambdaCutFunction<T>(T y) =>
        MamdaniCutFunction(y);

    double? IFuzzyImplication.LambdaCutLeftEndpoint<T>(T y) =>
        MamdaniCutLeftEndpoint(y);

    double? IFuzzyImplication.LambdaCutRightEndpoint<T>(T y) =>
        MamdaniCutRightEndpoint(y);

    (double X1, double X2)? IFuzzyImplication.LambdaCutInterval<T>(T y) =>
        MamdaniCutInterval(y);

    double IFuzzyImplication.CalculateArea<T>(T y, double errorMargin) =>
        MamdaniCutArea(y, errorMargin);

    double IFuzzyImplication.CentroidXCoordinate<T>(T y, double errorMargin) =>
        MamdaniCentroidXCoordinate(y, errorMargin);

    double IFuzzyImplication.CentroidYCoordinate<T>(T y, double errorMargin) =>
        MamdaniCentroidYCoordinate(y, errorMargin);

    (double X, double Y) IFuzzyImplication.CalculateCentroid<T>(T y, double errorMargin) =>
        MamdaniCutCentroid(y, errorMargin);

    Func<double, double> LambdaCutFunction<T>(T y, InferenceMethod method = Mamdani)
        where T : struct, IFuzzyNumber<T> => method switch
    {
        Mamdani => MamdaniCutFunction(y),
        Larsen => LarsenCutFunction(y),
        _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
    };

    (double X1, double X2)? LambdaCutInterval<T>(T y, InferenceMethod method = Mamdani)
        where T : struct, IFuzzyNumber<T> => method switch
    {
        Mamdani => MamdaniCutInterval(y),
        Larsen => MaxHeightInterval(),
        _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
    };

    double CalculateArea<T>(T y, double errorMargin = DefaultErrorMargin, InferenceMethod method = Mamdani)
        where T : struct, IFuzzyNumber<T> => method switch
    {
        Mamdani => MamdaniCutArea(y, errorMargin),
        Larsen => LarsenCutArea(y, errorMargin),
        _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
    };

    (double X, double Y) CalculateCentroid<T>(T y, double errorMargin = DefaultErrorMargin,
        InferenceMethod method = Mamdani) where T : struct, IFuzzyNumber<T> => method switch
    {
        Mamdani => MamdaniCutCentroid(y, errorMargin),
        Larsen => LarsenCutCentroid(y, errorMargin),
        _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
    };
}

public enum InferenceMethod
{
    Mamdani = 1,
    Larsen = 2
}
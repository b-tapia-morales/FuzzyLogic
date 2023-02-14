using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Implication;
using FuzzyLogic.Function.Interface;
using static System.Math;

namespace FuzzyLogic.Function.Real;

public class GaussianFunction : BaseGaussianFunction<double>, IFuzzyInference
{
    public GaussianFunction(string name, double m, double o, double h = 1) : base(name, m, o, h)
    {
    }

    public override double SupportLeftEndpoint() => double.NegativeInfinity;

    public override double SupportRightEndpoint() => double.PositiveInfinity;

    public override double MaxHeightLeftEndpoint() => M;

    public override double MaxHeightRightEndpoint() => M;

    double IClosedShape.CentroidXCoordinate(double errorMargin) => M;

    double? IMamdaniMinimum.MamdaniCutLeftEndpoint<T>(T y) =>
        y > H ? null : M - O * Sqrt(2 * Log(Min(H, y) / y));

    double? IMamdaniMinimum.MamdaniCutRightEndpoint<TNumber>(TNumber y) =>
        y > H ? null : M + O * Sqrt(2 * Log(Min(H, y) / y));

    double IMamdaniMinimum.MamdaniCentroidXCoordinate<TNumber>(TNumber y, double errorMargin) => M;

    double ILarsenProduct.LarsenCentroidXCoordinate<TNumber>(TNumber y, double errorMargin) => M;
}
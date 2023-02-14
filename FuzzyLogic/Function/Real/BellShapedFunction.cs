using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Implication;
using FuzzyLogic.Function.Interface;
using static System.Math;

namespace FuzzyLogic.Function.Real;

public class BellShapedFunction : BaseBellShapedFunction<double>, IFuzzyInference
{
    public BellShapedFunction(string name, double a, double b, double c, double h) : base(name, a, b, c, h)
    {
    }

    public BellShapedFunction(string name, double a, double b, double c) : base(name, a, b, c)
    {
    }

    public override double SupportLeftEndpoint() => double.NegativeInfinity;

    public override double SupportRightEndpoint() => double.PositiveInfinity;

    public override double MaxHeightLeftEndpoint() => C;

    public override double MaxHeightRightEndpoint() => C;

    double IClosedShape.CentroidXCoordinate(double errorMargin) => C;

    // c - a × ((H - λ) / λ) ^ (1 / 2b)
    double? IMamdaniMinimum.MamdaniCutLeftEndpoint<T>(T y) =>
        y > H ? null : C - A * Pow((H - Min(y, H)) / y, 1 / (2 * B));

    // c + a × ((H - λ) / λ) ^ (1 / 2b)
    double? IMamdaniMinimum.MamdaniCutRightEndpoint<T>(T y) =>
        y > H ? null : C + A * Pow((H - Min(y, H)) / y, 1 / (2 * B));

    double IMamdaniMinimum.MamdaniCentroidXCoordinate<TNumber>(TNumber y, double errorMargin) => C;

    double ILarsenProduct.LarsenCentroidXCoordinate<TNumber>(TNumber y, double errorMargin) => C;
}
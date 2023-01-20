using System.Numerics;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Function.Implication;

public interface ILarsenProduct<T> : IFuzzyImplication<T> where T : unmanaged, INumber<T>, IConvertible
{
    Func<T, double> LambdaCutFunction(FuzzyNumber y) => HeightFunction(Min(y.Value, H.ToDouble(null)));

    Func<T, double> LambdaCutFunction(FuzzyNumber y, double x0, double x1) => x =>
        (x.ToDouble(null) < x0 || x.ToDouble(null) > x1) ? 0.0 : LambdaCutFunction(y).Invoke(x);
}
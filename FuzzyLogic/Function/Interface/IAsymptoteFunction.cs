using System.Numerics;

namespace FuzzyLogic.Function.Interface;

public interface IAsymptoteFunction<T>: IMembershipFunction<T> where T : unmanaged, INumber<T>, IConvertible
{
    const double MinLambdaCut = 1e-2;
    const double MaxLambdaCut = 1 - MinLambdaCut;
    
    T ApproximateLowerBoundary();

    T ApproximateUpperBoundary();

    (T x1, T x2) ApproximateBoundaryInterval() => (ApproximateLowerBoundary(), ApproximateUpperBoundary());
}
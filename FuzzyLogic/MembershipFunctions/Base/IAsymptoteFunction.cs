using System.Numerics;

namespace FuzzyLogic.MembershipFunctions.Base;

public interface IAsymptoteFunction<T>: IMembershipFunction<T> where T : unmanaged, INumber<T>, IConvertible
{
    T ApproximateLowerBoundary();

    T ApproximateUpperBoundary();

    (T x1, T x2) ApproximateBoundaries() => (ApproximateLowerBoundary(), ApproximateUpperBoundary());
}
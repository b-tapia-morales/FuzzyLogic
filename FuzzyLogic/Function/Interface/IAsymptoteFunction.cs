using System.Numerics;

namespace FuzzyLogic.Function.Interface;

public interface IAsymptoteFunction<T> : IMembershipFunction<T> where T : unmanaged, INumber<T>, IConvertible
{
    T Inflection { get; }

    bool IsMonotonicallyIncreasing();

    bool IsMonotonicallyDecreasing();

    bool IsUnimodal();

    T ApproxSupportLeft();

    T ApproxSupportRight();

    (T X0, T X1) ApproxSupportBoundary() => (ApproxSupportLeft(), ApproxSupportRight());

    T? ApproxCoreLeft();

    T? ApproxCoreRight();

    (T? x1, T? x2) ApproxCoreBoundary() => (ApproxSupportLeft(), ApproxSupportRight());

    (T X0, T X1) IMembershipFunction<T>.FiniteSupportBoundary() => ApproxSupportBoundary();
}
using FuzzyLogic.Function.Real;

namespace FuzzyLogic.Function.Interface;

public abstract class AsymptoteFunction(string name, double uMax) : MembershipFunction(name, uMax)
{
    public abstract double Inflection { get; }

    public abstract bool IsMonotonicallyIncreasing();

    public abstract bool IsMonotonicallyDecreasing();

    public abstract bool IsUnimodal();

    public abstract double ApproxSupportLeft();

    public abstract double ApproxSupportRight();

    public (double X0, double X1) ApproxSupportBoundary() => (ApproxSupportLeft(), ApproxSupportRight());

    public abstract double? ApproxCoreLeft();

    public abstract double? ApproxCoreRight();

    public (double? x1, double? x2) ApproxCoreBoundary() => (ApproxCoreLeft(), ApproxCoreRight());

    public override double SupportLeft() => double.NegativeInfinity;

    public override double SupportRight() => double.PositiveInfinity;

    public override (double X0, double X1) FiniteSupportBoundary() => ApproxSupportBoundary();
}
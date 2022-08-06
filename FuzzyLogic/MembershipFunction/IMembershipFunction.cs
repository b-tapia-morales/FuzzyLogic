namespace FuzzyLogic.MembershipFunction;

public interface IMembershipFunction
{
    double? LowerBoundary() => null;

    double? UpperBoundary() => null;

    FuzzyNumber MembershipDegree(double x);

    FuzzyNumber MembershipDegree(int x) => MembershipDegree((double) x);

    (double X, FuzzyNumber Y) ToPoint(double x) => (x, MembershipDegree(x));
    
    (double X, FuzzyNumber Y) ToPoint(int x) => (x, MembershipDegree(x));
}
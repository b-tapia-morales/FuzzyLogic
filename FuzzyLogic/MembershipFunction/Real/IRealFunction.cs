namespace FuzzyLogic.MembershipFunction.Real;

public interface IRealFunction : IMembershipFunction<double>
{
    DataType IMembershipFunction<double>.DataType() => MembershipFunction.DataType.Double;
}
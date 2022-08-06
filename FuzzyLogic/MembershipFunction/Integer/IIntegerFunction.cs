namespace FuzzyLogic.MembershipFunction.Integer;

public interface IIntegerFunction : IMembershipFunction<int>
{
    DataType IMembershipFunction<int>.DataType() => MembershipFunction.DataType.Integer;
}
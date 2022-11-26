using System.Reflection;

namespace FuzzyLogic.Variable;

public class WarningVariableException : Exception
{
    private const string Template =
        @"An attempt has been made to add to a linguistic variable a linguistic entry whose Membership Function's range is outside of the variable's closed interval.
The Linguistic Variable is: «{0}»; its Closed Interval is: {1}.
The Linguistic Entry is: «{2}»; The range and type of its Membership Function is: {3} and «{4}» respectively";

    public WarningVariableException(string variableName, (double, double) variableRange, string entryName,
        (double, double) functionRange, MemberInfo functionType) :
        base(string.Format(Template, variableName, variableRange.ToString(), entryName, functionRange.ToString(),
            functionType.Name))
    {
    }

    public WarningVariableException(string variableName, (double, double) variableRange, string entryName,
        (double, double) functionRange, MemberInfo functionType, Exception inner) :
        base(string.Format(Template, variableName, variableRange.ToString(), entryName, functionRange.ToString(),
            functionType.Name), inner)
    {
    }
}
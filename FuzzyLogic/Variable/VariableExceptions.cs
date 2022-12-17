using System.Reflection;

namespace FuzzyLogic.Variable;

public class EmptyVariableException : Exception
{
    private const string Template =
        "An attempt has been made to create a linguistic variable with a name that is either an empty or a whitespace string.";

    public EmptyVariableException() : base(Template)
    {
    }

    public EmptyVariableException(Exception inner) : base(Template, inner)
    {
    }
}

public class EmptyEntryException : Exception
{
    private const string Template =
        @"An attempt has been made to add to a linguistic variable a linguistic entry with a name that is either an empty or a whitespace string.
The Linguistic variable is: «{0}»";

    public EmptyEntryException() : base(Template)
    {
    }

    public EmptyEntryException(Exception inner) : base(Template, inner)
    {
    }
}

public class DuplicatedEntryException : Exception
{
    private const string Template =
        @"An attempt has been made to add to a linguistic variable a linguistic entry whose name is already in use by another linguistic entry.
The Linguistic variable is: «{0}»
The Linguistic entry is: «{1}»";

    public DuplicatedEntryException(string variableName, string entryName) : base(string.Format(Template, variableName,
        entryName))
    {
    }

    public DuplicatedEntryException(string variableName, string entryName, Exception inner) : base(
        string.Format(Template, variableName, entryName), inner)
    {
    }
}

public class VariableRangeException : Exception
{
    private const string Template =
        @"An attempt has been made to add to a linguistic variable a linguistic entry whose Membership Function's range is outside of the variable's closed interval.
The Linguistic Variable is: «{0}»; its Closed Interval is: {1}.
The Linguistic Entry is: «{2}»; The range and type of its Membership Function is: {3} and «{4}» respectively";

    public VariableRangeException(string variableName, (double, double) variableRange, string entryName,
        (double, double) functionRange, MemberInfo functionType) :
        base(string.Format(Template, variableName, variableRange.ToString(), entryName, functionRange.ToString(),
            functionType.Name))
    {
    }

    public VariableRangeException(string variableName, (double, double) variableRange, string entryName,
        (double, double) functionRange, MemberInfo functionType, Exception inner) :
        base(string.Format(Template, variableName, variableRange.ToString(), entryName, functionRange.ToString(),
            functionType.Name), inner)
    {
    }
}
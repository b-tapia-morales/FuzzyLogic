namespace FuzzyLogic.Variable;

public class EmptyVariableException : Exception
{
    public override string Message =>
        "An attempt has been made to create a linguistic variable with a name that is either an empty or a whitespace string.";

    public EmptyVariableException()
    {
    }

    public EmptyVariableException(string message) : base(message)
    {
    }

    public EmptyVariableException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class EmptyEntryException : Exception
{
    public override string Message =>
        "An attempt has been made to add to a linguistic variable a linguistic entry with a name that is either an empty or a whitespace string.";

    public EmptyEntryException()
    {
    }

    public EmptyEntryException(string message) : base(message)
    {
    }

    public EmptyEntryException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class DuplicatedEntryException : Exception
{
    public override string Message =>
        "An attempt has been made to add to a linguistic variable a linguistic entry whose name is already in use by another linguistic entry.";

    private const string Template = "The linguistic entry in question is: {0}";

    public DuplicatedEntryException()
    {
    }

    public DuplicatedEntryException(string variableName) : base(string.Format(Template, variableName))
    {
    }

    public DuplicatedEntryException(string variableName, Exception inner) : base(string.Format(Template, variableName),
        inner)
    {
    }
}
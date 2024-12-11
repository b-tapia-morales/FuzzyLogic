namespace FuzzyLogic.Proposition;

public class VariableNotFoundException : Exception
{
    private const string Template =
        """
        An attempt has been made to retrieve a linguistic variable that is not contained in the Linguistic Base.
        The Linguistic variable is: «{0}»
        """;

    public VariableNotFoundException(string variableName) : base(string.Format(Template, variableName))
    {
    }

    public VariableNotFoundException(string variableName, Exception inner) : base(string.Format(Template, variableName),
        inner)
    {
    }
}

public class EntryNotFoundException : Exception
{
    private const string Template =
        """
        An attempt has been made to retrieve a Linguistic Entry that is not contained in the Linguistic Variable.
        The Linguistic Variable is: «{0}»
        The Linguistic Entry is: «{1}»
        """;

    public EntryNotFoundException(string variableName, string entryName) :
        base(string.Format(Template, variableName, entryName))
    {
    }

    public EntryNotFoundException(string variableName, string entryName, Exception inner) :
        base(string.Format(Template, variableName, entryName), inner)
    {
    }
}
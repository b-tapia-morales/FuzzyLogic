namespace FuzzyLogic.Engine.Defuzzify;

public class InapplicableRulesException : Exception
{
    private const string Template = "There are no rules that are applicable from the facts provided as parameters";

    public InapplicableRulesException() : base(Template)
    {
    }

    public InapplicableRulesException(Exception inner) : base(Template, inner)
    {
    }
}

public class DefuzzifyException : Exception
{
    private const string Template =
        "Attempt to defuzzify was unsuccessful because the premise weight of all rules is 0";

    public DefuzzifyException() : base(Template)
    {
    }

    public DefuzzifyException(Exception inner) : base(Template, inner)
    {
    }
}
namespace FuzzyLogic.Engine.Defuzzify;

public class InapplicableRulesException : Exception
{
    private const string Template =
        "There must be at least one rule that is applicable from the facts provided as parameters";

    public InapplicableRulesException() : base(Template)
    {
    }

    public InapplicableRulesException(Exception inner) : base(Template, inner)
    {
    }
}

public class MismatchedConsequentException : Exception
{
    private const string Template = "The rules' consequents must all be equal";

    public MismatchedConsequentException() : base(Template)
    {
    }

    public MismatchedConsequentException(Exception inner) : base(Template, inner)
    {
    }
}
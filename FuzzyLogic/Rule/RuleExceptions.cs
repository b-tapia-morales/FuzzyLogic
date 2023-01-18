namespace FuzzyLogic.Rule;

public class MissingAntecedentException : Exception
{
    private const string Template =
        "The following rule creation policy has been violated: In order to append propositions with the connectives: AND, OR, THEN, there must already be a proposition with the IF connective";

    public MissingAntecedentException() : base(Template)
    {
    }

    public MissingAntecedentException(Exception inner) : base(Template, inner)
    {
    }
}

public class DuplicatedAntecedentException : Exception
{
    private const string Template =
        @"The following rule creation policy has been violated: There can be one and only one proposition with the IF connective.";

    public DuplicatedAntecedentException() : base(Template)
    {
    }

    public DuplicatedAntecedentException(Exception inner) : base(Template, inner)
    {
    }
}

public class FinalizedRuleException : Exception
{
    private const string Template =
        "The following rule creation policy has been violated: It is not possible to append more propositions after a proposition with the connective THEN has been appended, meaning that the rule is finalized.";

    public FinalizedRuleException() : base(Template)
    {
    }

    public FinalizedRuleException(Exception inner) : base(Template, inner)
    {
    }
}

public class NegatedConsequentException : Exception
{
    private const string Template =
        "The following rule creation policy has been violated: The proposition with the THEN connective cannot be in negated form.";

    public NegatedConsequentException() : base(Template)
    {
    }

    public NegatedConsequentException(Exception inner) : base(Template, inner)
    {
    }
}
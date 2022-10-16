using FuzzyLogic.Proposition.Enums;

namespace FuzzyLogic.Rule;

public class MissingAntecedentException : Exception
{
    public override string Message =>
        $@"The following rule creation policy has been violated:
In order to append propositions with the connectives: {Connective.And}, {Connective.Or}, {Connective.Then}, there must already be a proposition with the {Connective.If} connective";

    public MissingAntecedentException()
    {
    }

    public MissingAntecedentException(string message) : base(message)
    {
    }

    public MissingAntecedentException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class DuplicatedAntecedentException : Exception
{
    public override string Message =>
        $@"The following rule creation policy has been violated:
There can be one and only one proposition with the {Connective.If} connective.";

    public DuplicatedAntecedentException()
    {
    }

    public DuplicatedAntecedentException(string message) : base(message)
    {
    }

    public DuplicatedAntecedentException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class FinalizedRuleException : Exception
{
    public override string Message =>
        $@"The following rule creation policy has been violated:
It is not possible to append more propositions after a proposition with the connective {Connective.Then} has been appended, meaning that the rule is finalized.";

    public FinalizedRuleException()
    {
    }

    public FinalizedRuleException(string message) : base(message)
    {
    }

    public FinalizedRuleException(string message, Exception inner) : base(message, inner)
    {
    }
}
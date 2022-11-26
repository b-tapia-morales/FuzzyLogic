namespace FuzzyLogic.Knowledge.Rule;

public class InvalidRuleException : Exception
{
    public override string Message =>
        "An attempt has been made to add an invalid rule (it has no Antecedent, or Consequent, or neither) to the rule base.";

    public InvalidRuleException()
    {
    }

    public InvalidRuleException(string message) : base(message)
    {
    }

    public InvalidRuleException(string message, Exception inner) : base(message, inner)
    {
    }
}
namespace FuzzyLogic.Clause;

public class FuzzyCondition
{
    public ConnectiveToken ConnectiveToken { get; }

    public FuzzyCondition(ConnectiveToken connectiveToken)
    {
        ConnectiveToken = connectiveToken;
    }
}
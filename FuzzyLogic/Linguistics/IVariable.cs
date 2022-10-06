using FuzzyLogic.Condition;
using FuzzyLogic.MembershipFunctions.Base;
using FuzzyLogic.MembershipFunctions.Real;

namespace FuzzyLogic.Linguistics;

public interface IVariable
{
    public string Name { get; }
    public ICollection<IRealFunction> LinguisticValues { get; }

    bool ContainsLinguisticValue(string name);

    IRealFunction? RetrieveLinguisticValue(string name);

    void AddAll(ICollection<IRealFunction> membershipFunctions);
    
    void AddTrapezoidFunction(string name, double a, double b, double c, double d);

    void AddTriangularFunction(string name, double a, double b, double c);

    void AddRectangularFunction(string name, double a, double b);

    void AddGaussianFunction(string name, double m, double o);

    void AddCauchyFunction(string name, double a, double b, double c);

    void AddSigmoidFunction(string name, double a, double c);

    public ICondition Is(string linguisticValue, HedgeToken token = HedgeToken.None);

    public ICondition IsNot(string linguisticValue, HedgeToken token = HedgeToken.None);
}
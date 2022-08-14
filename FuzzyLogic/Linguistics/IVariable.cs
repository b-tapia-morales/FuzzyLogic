using FuzzyLogic.Condition;
using FuzzyLogic.MembershipFunctions.Base;
using FuzzyLogic.MembershipFunctions.Real;

namespace FuzzyLogic.Linguistics;

public interface IVariable
{
    public string Name { get; }
    public List<IRealFunction> LinguisticValues { get; }

    void AddTrapezoidFunction(string name, double a, double b, double c, double d);

    void AddTriangularFunction(string name, double a, double b, double c);

    void AddRectangularFunction(string name, double a, double b);

    void AddGaussianFunction(string name, double m, double o);

    void AddSigmoidFunction(string name, double a, double c);

    public ICondition Is(string linguisticValue);

    public ICondition IsNot(string linguisticValue);
}
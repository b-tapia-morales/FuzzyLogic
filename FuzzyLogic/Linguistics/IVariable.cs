using FuzzyLogic.MembershipFunctions.Base;
using FuzzyLogic.MembershipFunctions.Real;

namespace FuzzyLogic.Linguistics;

public interface IVariable
{
    public string Name { get; }
    public IDictionary<string, IRealFunction> LinguisticEntries { get; }

    IVariable AddAll(IDictionary<string, IRealFunction> linguisticEntries);
    
    IVariable AddTrapezoidFunction(string name, double a, double b, double c, double d);

    IVariable AddTriangularFunction(string name, double a, double b, double c);

    IVariable AddGaussianFunction(string name, double m, double o);

    IVariable AddCauchyFunction(string name, double a, double b, double c);

    IVariable AddSigmoidFunction(string name, double a, double c);

    IVariable AddFunction(string name, IRealFunction function);
    
    bool ContainsLinguisticEntry(string name);

    IRealFunction? RetrieveLinguisticEntry(string name);
}
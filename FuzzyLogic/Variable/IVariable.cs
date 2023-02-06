using FuzzyLogic.Function.Interface;

namespace FuzzyLogic.Variable;

/// <summary>
///     <para>
///         A class representation for a linguistic variable. A linguistic variable is uniquely identifiable by name, and
///         it usually has a set of linguistic entries or values, with each linguistic entry being identifiable from one
///         another by its name.
///     </para>
///     <para>
///         A linguistic variable might also have a closed interval of real numbers for <i>x</i> values. This interval can be
///         specified at instantiation by calling the <see cref="LinguisticVariable.Create(string,double,double)"/> method,
///         which takes the two real values passed as parameters as its two limit points.
///     </para>
///     <para>
///         If the <see cref="LinguisticVariable.Create(string)"/> method is called instead, the linguistic variable will
///         have an open interval, only enclosed by the minimum and maximum possible values for a double
///         (<see cref="double.MinValue"/> and <see cref="double.MaxValue"/> respectively).
/// </para>
/// </summary>
public interface IVariable
{
    public double LowerBoundary { get; }
    public double UpperBoundary { get; }
    public bool IsClosed { get; }
    public string Name { get; }
    public IDictionary<string, IMembershipFunction<double>> LinguisticEntries { get; }

    IVariable AddAll(IDictionary<string, IMembershipFunction<double>> linguisticEntries);

    IVariable AddTrapezoidFunction(string name, double a, double b, double c, double d, double h = 1);

    IVariable AddLeftTrapezoidFunction(string name, double a, double b, double h = 1);

    IVariable AddRightTrapezoidFunction(string name, double a, double b, double h = 1);

    IVariable AddTriangularFunction(string name, double a, double b, double c, double h = 1);

    IVariable AddGaussianFunction(string name, double m, double o, double h = 1);

    IVariable AddCauchyFunction(string name, double a, double b, double c, double h = 1);

    IVariable AddSigmoidFunction(string name, double a, double c, double h = 1);

    IVariable AddFunction(string name, IMembershipFunction<double> function);

    bool ContainsLinguisticEntry(string name);

    IMembershipFunction<double>? RetrieveLinguisticEntry(string name);
}
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Variable;

namespace FuzzyLogic.Knowledge.Linguistic;

/// <summary>
/// A class representation for a data-storage base in which all linguistic variables and their corresponding
/// linguistic entries and membership functions are stored.
/// </summary>
public interface ILinguisticBase
{
    /// <summary>
    /// The collection where the linguistic variables will be stored.
    /// </summary>
    IDictionary<string, IVariable> LinguisticVariables { get; }

    /// <summary>
    /// Determines whether the base contains a linguistic variable with the <see cref="IVariable.Name" /> provided as a
    /// parameter.
    /// </summary>
    /// <param name="name">The name of the linguistic variable</param>
    /// <returns>true if the base contains a linguistic variable with the given name; otherwise, false.</returns>
    /// <seealso cref="IVariable.Name" />
    bool ContainsVariable(string name);

    /// <summary>
    /// Retrieves the linguistic variable with the <see cref="IVariable.Name" /> provided as a parameter.
    /// </summary>
    /// <param name="name">The name of the linguistic variable</param>
    /// <returns>The linguistic variable itself if the base contains linguistic variable with the given name; otherwise, null.</returns>
    /// <seealso cref="IVariable.Name" />
    IVariable? RetrieveVariable(string name);

    bool TryGetVariable(string name, out IVariable? variable);

    /// <summary>
    /// Verifies whether both of the following conditions are satisfied:
    /// <list type="number">
    /// <item>
    /// <description>
    /// A linguistic variable with the name provided as the first parameter exists in the linguistic base.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// The specified linguistic variable contains a linguistic term with the name provided as the second parameter.
    /// </description>
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="variable">The name of the linguistic variable</param>
    /// <param name="term">The name of the linguistic term contained in the linguistic variable</param>
    /// <returns>
    /// <see langword="true" /> if the linguistic base contains the specified variable,
    /// and the variable contains the specified term;
    /// otherwise, <see langword="false" />.
    /// </returns>
    /// <seealso cref="LinguisticVariables" />
    /// <seealso cref="IVariable.SemanticalMappings" />
    bool ContainsFunction(string variable, string term);

    /// <summary>
    /// Retrieves the membership function associated with the specified linguistic term
    /// only if both of the following conditions are satisfied:
    /// <list type="number">
    /// <item>
    /// <description>
    /// A linguistic variable with the name provided as the first parameter exists in the linguistic base.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// The specified linguistic variable contains a linguistic term with the name provided as the second parameter.
    /// </description>
    /// </item>
    /// </list>
    /// Otherwise; returns null.
    /// </summary>
    /// <param name="variable">The name of the linguistic variable</param>
    /// <param name="term">The name of the linguistic term contained in the linguistic variable</param>
    /// <returns>
    /// The linguistic term if the conditions described above are met; otherwise, <see langword="null" />.
    /// </returns>
    /// <seealso cref="LinguisticVariables" />
    /// <seealso cref="IVariable.SemanticalMappings" />
    IMembershipFunction? RetrieveFunction(string variable, string term);

    bool TryGetFunction(string variable, string term, out IMembershipFunction? function);

    void Add(IVariable variable);

    void AddAll(ICollection<IVariable> variables);

    void AddAll(params IEnumerable<IVariable> variables) => AddAll(variables.ToList());
}
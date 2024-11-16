﻿using FuzzyLogic.Function.Interface;
using FuzzyLogic.Variable;

namespace FuzzyLogic.Knowledge.Linguistic;

/// <summary>
///     A class representation for a data-storage base in which all linguistic variables and their corresponding
///     linguistic entries and membership functions are stored.
/// </summary>
public interface ILinguisticBase
{
    /// <summary>
    ///     The collection where the linguistic variables will be stored.
    /// </summary>
    public IDictionary<string, IVariable> LinguisticVariables { get; }

    /// <summary>
    ///     Determines whether the base contains a linguistic variable with the <see cref="IVariable.Name" /> provided as a
    ///     parameter.
    /// </summary>
    /// <param name="name">The name of the linguistic variable</param>
    /// <returns>true if the base contains a linguistic variable with the given name; otherwise, false.</returns>
    /// <seealso cref="IVariable.Name" />
    bool Contains(string name);

    /// <summary>
    ///     Retrieves the linguistic variable with the <see cref="IVariable.Name" /> provided as a parameter.
    /// </summary>
    /// <param name="name">The name of the linguistic variable</param>
    /// <returns>The linguistic variable itself if the base contains linguistic variable with the given name; otherwise, null.</returns>
    /// <seealso cref="IVariable.Name" />
    IVariable? Retrieve(string name);

    /// <summary>
    ///     Determines whether the base contains a linguistic variable with the name provided as the first parameter;
    ///     <b>IF</b> it does, determines whether said linguistic variable contains a linguistic entry with the name
    ///     provided as the second parameter.
    /// </summary>
    /// <param name="variableName">The name of the linguistic variable</param>
    /// <param name="entryName">The name of the linguistic entry contained in the linguistic variable</param>
    /// <returns>
    ///     true if the base contains a linguistic variable <b>AND</b> if said linguistic variable contains a linguistic
    ///     entry with the given names; otherwise, false.
    /// </returns>
    /// <seealso cref="LinguisticVariables" />
    /// <seealso cref="IVariable.LinguisticEntries" />
    bool ContainsLinguisticEntry(string variableName, string entryName) =>
        RetrieveLinguisticEntry(variableName, entryName) != null;

    /// <summary>
    ///     Retrieves a linguistic entry contained if the following two conditions are met:
    ///     <list type="number">
    ///         <item>
    ///             <description>
    ///                 There is a linguistic variable contained in the base with the name provided as the first parameter.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 There is a linguistic entry contained in said linguistic variable with the name provided as the second
    ///                 parameter.
    ///             </description>
    ///         </item>
    ///     </list>
    ///     Otherwise; returns null.
    /// </summary>
    /// <param name="variableName">The name of the variable contained in the linguistic base</param>
    /// <param name="entryName">The name of the entry contained in the linguistic variable</param>
    /// <returns>
    ///     The linguistic entry if the conditions described above are met; otherwise, null.
    /// </returns>
    /// <seealso cref="LinguisticVariables" />
    /// <seealso cref="IVariable.LinguisticEntries" />
    IMembershipFunction<double>? RetrieveLinguisticEntry(string variableName, string entryName) =>
        Retrieve(variableName)?.RetrieveLinguisticEntry(entryName);

    void Add(IVariable variable);

    void AddAll(ICollection<IVariable> variables);

    void AddAll(params IEnumerable<IVariable> variables) => AddAll(variables.ToList());

    /// <summary>
    /// Creates a new instance of a <see cref="ILinguisticBase"/>.
    /// </summary>
    /// <returns>A new instance of a <see cref="ILinguisticBase"/></returns>
    static abstract ILinguisticBase Create();

    /// <summary>
    /// Creates a new instance of a
    /// <see cref="ILinguisticBase"/> that contains the collection of linguistic variables
    /// provided as a parameter.
    /// </summary>
    /// <param name="variables">A collection of linguistic variables</param>
    /// <returns>The linguistic base itself containing the collection of linguistic variables</returns>
    static abstract ILinguisticBase Create(ICollection<IVariable> variables);

    /// <summary>
    /// Creates a new instance of a
    /// <see cref="ILinguisticBase"/> that contains all the linguistic variables provided as parameters.
    /// </summary>
    /// <param name="variables">A varying number of linguistic variables</param>
    /// <returns>The linguistic base itself containing the linguistic variables</returns>
    static abstract ILinguisticBase Create(params IEnumerable<IVariable> variables);

    /// <summary>
    ///     <para>
    ///         This method is marked to indicate that, if the necessity of creating a new instance of a
    ///         <see cref="ILinguisticBase" /> with existing data arises, it must be performed with this very method.
    ///     </para>
    ///     <para>
    ///         To do this, a new class must be declared, and it must extend a class that implements this very
    ///         interface, also implementing and hiding this very method by using the <see langword="new" /> keyword.
    ///     </para>
    ///     <para>
    ///         Note that this method, as it is currently implemented, does nothing by itself other than creating a new
    ///         instance of a <see cref="ILinguisticBase" />.
    ///     </para>
    /// </summary>
    /// <returns>A new instance of a <see cref="ILinguisticBase" /> with existing data.</returns>
    /// <seealso cref="Create()" />
    static abstract ILinguisticBase Initialize();
}
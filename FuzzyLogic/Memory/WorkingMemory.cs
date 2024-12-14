using FuzzyLogic.Utils.Csv;
using static FuzzyLogic.Memory.EntryResolutionMethod;

// ReSharper disable RedundantExplicitTupleComponentName

namespace FuzzyLogic.Memory;

public class WorkingMemory : IWorkingMemory
{
    private WorkingMemory(EntryResolutionMethod method = Replace)
    {
        Facts = new Dictionary<string, double>();
        Method = method;
    }

    private WorkingMemory(IDictionary<string, double> facts, EntryResolutionMethod method = Replace)
    {
        Facts = facts;
        Method = method;
    }

    public IDictionary<string, double> Facts { get; }
    public EntryResolutionMethod Method { get; }

    /// <summary>
    /// Creates an instance of a <see cref="IWorkingMemory"/> with an empty collection of facts.
    /// </summary>
    /// <param name="method">
    /// The <see cref="EntryResolutionMethod"/> to handle collisions when adding facts
    /// to the working memory.
    /// Defaults to <see cref="EntryResolutionMethod.Replace"/>.
    /// </param>
    /// <returns>An instance of a Working Memory with no pre-existing facts.</returns>
    public static IWorkingMemory Create(EntryResolutionMethod method = Replace) =>
        new WorkingMemory(method);

    /// <summary>
    /// Creates an instance of a <see cref="IWorkingMemory"/> with a specified collection of facts.
    /// </summary>
    /// <param name="facts">
    /// A dictionary representing the initial facts, where the keys correspond to the linguistic variables' names,
    /// and the values correspond to their associated crisp values.</param>
    /// <param name="method">
    /// The <see cref="EntryResolutionMethod"/> used to handle name collisions when adding facts.
    /// A collision occurs when a new fact shares the same linguistic variable name as an existing fact.
    /// <list type="bullet">
    /// <item>
    /// <see cref="EntryResolutionMethod.Preserve"/>: Retains the value of the existing fact.
    /// </item>
    /// <item>
    /// <see cref="EntryResolutionMethod.Replace"/>: Overwrites the existing fact with the new value.
    /// This is the default behavior.
    /// </item>
    /// </list>
    /// </param>
    /// <returns>An instance of a Working Memory initialized with the specified pre-existing facts.</returns>
    public static IWorkingMemory Create(IDictionary<string, double> facts, EntryResolutionMethod method = Replace) =>
        new WorkingMemory(facts, method);

    /// <summary>
    /// Creates an instance of a <see cref="IWorkingMemory"/> with a variable number of tuples,
    /// where each tuple represents a fact, with its linguistic variable name as <b>Key</b>,
    /// and its crisp value as <b>Value</b>.
    /// </summary>
    /// <param name="method">
    /// The <see cref="EntryResolutionMethod"/> used to handle name collisions when adding facts.
    /// A collision occurs when two tuples share the same linguistic variable name as a Key.
    /// <list type="bullet">
    /// <item>
    /// <see cref="EntryResolutionMethod.Preserve"/>: Retains the first tuple's Value.
    /// </item>
    /// <item>
    /// <see cref="EntryResolutionMethod.Replace"/>: Retains the last tuple's Value.
    /// </item>
    /// </list>
    /// </param>
    /// <param name="facts">
    /// A variable number of parameters as tuples, where each tuple represents a fact
    /// with its linguistic variable name as <b>Key</b>, and its crisp value as <b>Value</b>.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="IWorkingMemory"/> initialized with the specified pre-existing facts.
    /// </returns>
    /// <example>
    /// <code>WorkingMemory.Create(EntryResolutionMethod.Replace,
    /// ("Age", 18), ("Height", 175), ("Weight", 70));</code>
    /// </example>
    /// <remarks>
    /// The described behavior of handling collisions also extends to new facts being added after the instantiation.
    /// </remarks>
    /// <seealso cref="WorkingMemory.Create(IDictionary{String,Double},EntryResolutionMethod)"/>
    public static IWorkingMemory Create(EntryResolutionMethod method, params IEnumerable<(string Key, double Value)> facts) =>
        new WorkingMemory(facts
            .GroupBy(tuple => tuple.Key)
            .Select(group => (Key: group.Key, List: group.Select(e => e.Value)))
            .ToDictionary(tuple => tuple.Key, tuple => method == Replace ? tuple.List.Last() : tuple.List.First()));

    /// <summary>
    /// Creates an instance of a <see cref="IWorkingMemory"/> with a variable number of tuples,
    /// where each tuple represents a fact, with its linguistic variable name as <b>Key</b>,
    /// and its crisp value as <b>Value</b>.
    /// In the case that two tuples share the same linguistic variable name as a Key,
    /// the last tuple's value will be used.
    /// </summary>
    /// <returns>
    /// A new instance of <see cref="IWorkingMemory"/> initialized with the specified pre-existing facts.
    /// </returns>
    /// <example>
    /// <code>WorkingMemory.Create(("Age", 18), ("Height", 175), ("Weight", 70));</code>
    /// </example>
    /// <remarks>
    /// The described behavior of handling collisions also extends to new facts being added after the instantiation.
    /// </remarks>
    /// <seealso cref="WorkingMemory.Create(EntryResolutionMethod,IEnumerable{ValueTuple{String,Double}})"/>
    public static IWorkingMemory Create(params IEnumerable<(string Key, double Value)> facts) =>
        Create(Replace, facts);

    /// <summary>
    /// Creates an instance of a <see cref="IWorkingMemory"/> populated with initial facts extracted from a CSV file.
    /// </summary>
    /// <param name="folderPath">
    /// The path to the folder containing the CSV file.
    /// The file must conform to the expected structure for loading facts.
    /// </param>
    /// <param name="hasHeader">
    /// Indicates whether the CSV file contains a header row.
    /// If <c>true</c>, the first row is skipped during processing.
    /// Defaults to <c>false</c>.
    /// </param>
    /// <param name="delimiter">
    /// Specifies the type of delimiter used in the CSV file to separate values.
    /// Supported delimiters include <see cref="DelimiterType.Comma"/> (<c>,</c>),
    /// <see cref="DelimiterType.Semicolon"/> (<c>;</c>), <see cref="DelimiterType.Tab"/> (<c>\t</c>),
    /// and <see cref="DelimiterType.Pipe"/> (<c>|</c>).
    /// Defaults to <see cref="DelimiterType.Comma"/>.
    /// </param>
    /// <param name="method">
    /// The <see cref="EntryResolutionMethod"/> used to handle name collisions when adding facts.
    /// A collision occurs when two entries use the same linguistic variable name.
    /// <list type="bullet">
    /// <item>
    /// <see cref="EntryResolutionMethod.Preserve"/>: Retains the first entry's value.
    /// </item>
    /// <item>
    /// <see cref="EntryResolutionMethod.Replace"/>: Retains the last entry's value.
    /// </item>
    /// </list>
    /// </param>
    /// <returns>
    /// A new <see cref="IWorkingMemory"/> instance populated with the facts from the specified CSV file.
    /// </returns>
    /// <remarks>
    /// The described behavior of handling collisions also extends to new facts being added after the instantiation.
    /// </remarks>
    /// <exception cref="FileNotFoundException">
    /// Thrown if no CSV file is found at the specified <paramref name="folderPath"/>.
    /// </exception>
    /// <example>
    /// <p>An example of the expected structure for a CSV file that represents facts is as follows:</p>
    /// <p><c>Age,18</c></p>
    /// <p><c>Height,175</c></p>
    /// <p><c>Weight,70</c></p>
    /// </example>
    public static IWorkingMemory CreateFromFile(string folderPath, bool hasHeader = false,
        DelimiterType delimiter = DelimiterType.Comma, EntryResolutionMethod method = Replace) =>
        new WorkingMemory(RowRetrieval.RetrieveRows<FactRow, FactMapping>(folderPath, hasHeader, delimiter)
            .GroupBy(tuple => tuple.Key)
            .Select(group => (Key: group.Key, List: group.Select(e => e.Value)))
            .ToDictionary(tuple => tuple.Key, tuple => method == Replace ? tuple.List.Last() : tuple.List.First()));

    public bool ContainsFact(string key) => Facts.ContainsKey(key);

    public double? RetrieveValue(string key) => Facts.TryGetValue(key, out var value) ? value : null;

    public void AddFact(string key, double value)
    {
        if (!Facts.TryAdd(key, value) && Method == Replace)
        {
            Facts[key] = value;
        }
    }

    public void UpdateFact(string key, double value)
    {
        if (!Facts.TryAdd(key, value))
        {
            Facts[key] = value;
        }
    }

    public bool RemoveFact(string key)
    {
        return Facts.Remove(key);
    }

    public override string ToString() => string.Join(Environment.NewLine, Facts.Select(e => $"{e.Key} = {e.Value}"));
}
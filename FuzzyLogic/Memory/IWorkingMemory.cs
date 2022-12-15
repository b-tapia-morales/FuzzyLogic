using FuzzyLogic.Utils.Csv;
using static FuzzyLogic.Memory.EntryResolutionMethod;

namespace FuzzyLogic.Memory;

/// <summary>
///     Contains the facts that are provided by the user or inferred from other rules. The implementing classes must
///     provide the facilities of creating new values, updating old values or removing existing values.
/// </summary>
public interface IWorkingMemory
{
    /// <summary>
    ///     The collection where the entries will be stored.
    /// </summary>
    public IDictionary<string, double> Facts { get; }

    /// <summary>
    ///     The method that resolves conflicting entries with the same declared key.
    /// </summary>
    public EntryResolutionMethod Method { get; }

    static abstract IWorkingMemory Create(EntryResolutionMethod method = Replace);

    static abstract IWorkingMemory Create(IDictionary<string, double> facts,
        EntryResolutionMethod method = Replace);

    static abstract IWorkingMemory InitializeFromFile(string folderPath, EntryResolutionMethod method = Replace);

    IWorkingMemory Clone();

    /// <summary>
    ///     Checks if there is an entry stored with the key provided as a parameter.
    /// </summary>
    /// <param name="key">The entry's key.</param>
    /// <returns>
    ///     <see langword="true" /> if there is such entry; <see langword="false" /> otherwise.
    /// </returns>
    bool ContainsFact(string key);

    /// <summary>
    ///     Retrieves the entry stored with the key provided as a parameter, if there is such entry. Otherwise, it
    ///     defaults to <see langword="null" />.
    /// </summary>
    /// <param name="key">The entry's key.</param>
    /// <returns>The entry with the key provided as a parameter.</returns>
    double? RetrieveValue(string key);

    /// <summary>
    ///     Saves an entry with the key and value provided as parameters. If there is an entry with the same key already
    ///     stored, and the <see cref="Method" /> is set to <see cref="EntryResolutionMethod.Replace" /> mode, the
    ///     entry's associated value will be replaced by the new value provided.
    /// </summary>
    /// <param name="key">The new entry's key.</param>
    /// <param name="value">The new entry's value.</param>
    void AddFact(string key, double value);

    /// <summary>
    ///     Updates the entry stored with the key provided as a parameter by replacing its associated value with the new
    ///     value provided, if there is such entry. Otherwise, it defaults to saving it as a new entry.
    /// </summary>
    /// <param name="key">The entry's key.</param>
    /// <param name="value">The entry's value.</param>
    void UpdateFact(string key, double value);

    /// <summary>
    ///     Removes the entry stored with the key provided as a parameter, if there is such entry. Otherwise, it does
    ///     nothing.
    /// </summary>
    /// <param name="key">The entry's key.</param>
    /// <returns><see langword="true" /> if there is such entry; <see langword="false" /> otherwise.</returns>
    bool RemoveFact(string key);
}

/// <summary>
///     Represents the resolution method for new entries whose keys collide with previous entries' keys in the
///     <see cref="IWorkingMemory" /> class.
/// </summary>
public enum EntryResolutionMethod
{
    /// <summary>
    ///     Indicates that the previous entries will be preserved.
    /// </summary>
    Preserve = 1,

    /// <summary>
    ///     Indicates that the new entries will replace the previous ones.
    /// </summary>
    Replace = 2
}
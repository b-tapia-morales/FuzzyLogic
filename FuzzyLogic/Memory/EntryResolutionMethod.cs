namespace FuzzyLogic.Memory;

/// <summary>
/// Represents the resolution method for new entries whose keys collide with previous entries' keys in the
/// <see cref="IWorkingMemory"/> class.
/// </summary>
public enum EntryResolutionMethod
{
    /// <summary>
    /// Indicates that the previous entries will be preserved.
    /// </summary>
    Keep = 1,
    /// <summary>
    /// Indicates that the new entries will replace the previous ones.
    /// </summary>
    Replace = 2,
}
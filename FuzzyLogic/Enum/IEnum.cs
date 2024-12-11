using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace FuzzyLogic.Enum;

public interface IEnum<out T, TEnum>
    where T : SmartEnum<T>, IEnum<T, TEnum>
    where TEnum : struct, System.Enum, IConvertible
{
    static IReadOnlyList<T> Values { get; } = SmartEnum<T>.List.OrderBy(e => e.Value).ToImmutableList();
    string ReadableName { get; }

    static T ToValue(TEnum @enum) =>
        EnumDict.TryGetValue(@enum, out var value) ? value : throw new KeyNotFoundException();

    static T ToValue(string readableName) =>
        ReadableNameDict.TryGetValue(readableName, out var value) ? value : throw new KeyNotFoundException();

    static bool TryGetValue(TEnum @enum, out T? value) =>
        EnumDict.TryGetValue(@enum, out value);

    static bool TryGetValue(string readableName, out T? value) =>
        ReadableNameDict.TryGetValue(readableName, out value);

    static TEnum ToEnum(T value) =>
        ValueDict.TryGetValue(value, out var token) ? token : throw new KeyNotFoundException();

    static bool TryGetEnum(T value, out TEnum? token)
    {
        token = ValueDict.TryGetValue(value, out var enumValue) ? enumValue : null;
        return token != null;
    }

    private static readonly ImmutableList<TEnum> Enums =
        System.Enum.GetValues<TEnum>().OrderBy(e => e.ToInt32(null)).ToImmutableList();

    private static IReadOnlyDictionary<TEnum, T> EnumDict { get; } =
        Enums.Zip(Values, (k, v) => new {Key = k, Value = v}).ToImmutableSortedDictionary(e => e.Key, e => e.Value);

    private static IReadOnlyDictionary<T, TEnum> ValueDict { get; } =
        Enums.Zip(Values, (k, v) => new {Key = k, Value = v}).ToImmutableSortedDictionary(e => e.Value, e => e.Key);

    private static IReadOnlyDictionary<string, T> ReadableNameDict { get; } =
        Values.ToImmutableSortedDictionary(e => e.ReadableName, e => e, StringComparer.InvariantCultureIgnoreCase);
}
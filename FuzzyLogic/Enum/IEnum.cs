using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace FuzzyLogic.Enum;

public interface IEnum<out T, TEnum>
    where T : SmartEnum<T>, IEnum<T, TEnum>
    where TEnum : struct, System.Enum, IConvertible
{
    private static readonly ImmutableList<TEnum> Tokens =
        System.Enum.GetValues<TEnum>().OrderBy(e => e.ToInt32(null)).ToImmutableList();

    static IReadOnlyList<T> Values { get; } = SmartEnum<T>.List.OrderBy(e => e.Value).ToImmutableList();

    static IReadOnlyList<T> NonNullValues { get; } = Values.Skip(1).ToImmutableList();

    static IReadOnlyDictionary<TEnum, T> TokenDictionary { get; } =
        Tokens.Zip(Values, (k, v) => new { Key = k, Value = v }).ToImmutableSortedDictionary(e => e.Key, e => e.Value);

    static IReadOnlyDictionary<T, TEnum> ReverseTokenDictionary { get; } =
        Tokens.Zip(Values, (k, v) => new { Key = k, Value = v }).ToImmutableSortedDictionary(e => e.Value, e => e.Key);

    string ReadableName { get; }

    static T ToValue(TEnum token) =>
        TokenDictionary.TryGetValue(token, out var value) ? value : throw new KeyNotFoundException();

    static bool TryGetValue(TEnum token, out T? value) =>
        TokenDictionary.TryGetValue(token, out value);

    static TEnum ToToken(T value) =>
        ReverseTokenDictionary.TryGetValue(value, out var token) ? token : throw new KeyNotFoundException();

    static bool TryGetToken(T value, out TEnum? token)
    {
        token = ReverseTokenDictionary.TryGetValue(value, out var enumValue) ? enumValue : null;
        return token != null;
    }
}
using CsvHelper.Configuration;

namespace FuzzyLogic.Memory;

public sealed class FactRow<T> where T : unmanaged, IConvertible
{
    public string Key { get; set; } = null!;
    public T Value { get; set; } = default;

    public override string ToString() => $"{Key} - {Value}";
}

public sealed class FactMapping<T> : ClassMap<FactRow<T>> where T : unmanaged, IConvertible
{
    public FactMapping()
    {
        Map(p => p.Key).Index(0);
        Map(p => p.Value).Index(1);
    }
}
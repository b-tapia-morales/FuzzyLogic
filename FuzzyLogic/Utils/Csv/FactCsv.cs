using CsvHelper.Configuration;

// ReSharper disable All

namespace FuzzyLogic.Utils.Csv;

public sealed class FactRow
{
    public string Key { get; set; } = null!;
    public double Value { get; set; } = 0;

    public override string ToString() => $"{Key} - {Value}";
}

public sealed class FactMapping : ClassMap<FactRow>
{
    public FactMapping()
    {
        Map(p => p.Key).Index(0);
        Map(p => p.Value).Index(1);
    }
}
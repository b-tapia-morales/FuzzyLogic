using System.Globalization;
using System.Text;
using Ardalis.SmartEnum;
using CsvHelper;
using CsvHelper.Configuration;
using FuzzyLogic.Enum;
using static FuzzyLogic.Utils.Csv.DelimiterType;

namespace FuzzyLogic.Utils.Csv;

public static class RowRetrieval
{
    public static IEnumerable<T> RetrieveRows<T, TMap>(string filePath,
        bool hasHeader = false, DelimiterType type = Semicolon) where TMap : ClassMap
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = IEnum<Delimiter, DelimiterType>.ToValue(type).Character,
            HasHeaderRecord = hasHeader
        };

        using var textReader = new StreamReader(filePath, Encoding.UTF8);
        using var csv = new CsvReader(textReader, configuration);
        csv.Context.RegisterClassMap<TMap>();
        return csv.GetRecords<T>().ToList();
    }
}

public enum DelimiterType
{
    Comma,
    Semicolon,
    Pipe,
    Tab
}

public sealed class Delimiter : SmartEnum<Delimiter>, IEnum<Delimiter, DelimiterType>
{
    public static readonly Delimiter Comma = new(nameof(Comma), ",", (int) DelimiterType.Comma);

    public static readonly Delimiter Semicolon = new(nameof(Semicolon), ";", (int) DelimiterType.Semicolon);

    public static readonly Delimiter Pipe = new(nameof(Pipe), "|", (int) DelimiterType.Pipe);

    public static readonly Delimiter Tab = new(nameof(Tab), "\t", (int) DelimiterType.Tab);

    private Delimiter(string name, string character, int value) : base(name, value) => Character = character;

    public string Character { get; }

    public string ReadableName => Name;
}
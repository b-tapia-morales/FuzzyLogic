using System.Globalization;
using System.Reflection;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace FuzzyLogic.Memory;

public static class FactRetrieval
{
    public static ICollection<FactRow<T>> RetrieveRows<T>(string folderName, string fileName)
        where T : unmanaged, IConvertible
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), folderName, fileName);
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = ";",
            HasHeaderRecord = false
        };

        using var textReader = new StreamReader(path, Encoding.UTF8);
        using var csv = new CsvReader(textReader, configuration);
        csv.Context.RegisterClassMap<FactMapping<T>>();
        return csv.GetRecords<FactRow<T>>().ToList();
    }
}
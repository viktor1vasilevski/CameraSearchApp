using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Infrastructure.Data.Exceptions;
using System.Globalization;

namespace Infrastructure.Data.Context;

public class AppDbContext : IContext
{
    private readonly string _csvFilePath;
    private List<Camera>? _cache;

    public AppDbContext(string csvFilePath)
    {
        _csvFilePath = csvFilePath;
    }
    public async Task<List<Camera>> LoadDataAsync()
    {
        if (_cache != null && _cache.Count != 0)
            return _cache;

        try
        {
            using var reader = new StreamReader(_csvFilePath);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using var csv = new CsvReader(reader, config);
            var records = new List<Camera>();

            await foreach (var record in csv.GetRecordsAsync<Camera>())
            {
                records.Add(record);
            }

            _cache = records;

            return records;
        }
        catch (CsvHelperException ex)
        {
            throw new CsvParseException("Failed to parse CSV file.", ex);
        }
        catch (IOException ex)
        {
            throw new DataLoadException("Error accessing data file.", ex);
        }

    }
}

public interface IContext
{
    Task<List<Camera>> LoadDataAsync();
}
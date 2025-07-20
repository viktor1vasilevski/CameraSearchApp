using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Infrastructure.Data.Constants;
using Infrastructure.Exceptions.Csv;
using System.Globalization;

namespace Infrastructure.Data.Context;

public class AppDbContext : IContext
{
    private readonly string _csvFilePath;
    private List<Camera>? _cameraCache;

    public AppDbContext(string csvFilePath)
    {
        _csvFilePath = csvFilePath;
    }

    public async Task<List<TEntity>> LoadDataAsync<TEntity>() where TEntity : class
    {
        var cameras = await LoadCamerasAsync();
        return cameras.Cast<TEntity>().ToList();
    }

    private async Task<List<Camera>> LoadCamerasAsync()
    {
        if (_cameraCache != null && _cameraCache.Count > 0)
            return _cameraCache;

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

            _cameraCache = records;
            return records;
        }
        catch (CsvHelperException ex)
        {
            throw new CsvParseException(CsvConstants.FailParseCsvFile, ex);
        }
        catch (IOException ex)
        {
            throw new DataLoadException(CsvConstants.FormatInvalid, ex);
        }
    }
}

public interface IContext
{
    Task<List<TEntity>> LoadDataAsync<TEntity>() where TEntity : class;
}

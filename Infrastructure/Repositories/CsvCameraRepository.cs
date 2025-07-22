using Application.Interfaces.Repositories;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Infrastructure.Data.Constants;
using Infrastructure.Exceptions.Csv;
using System.Globalization;

namespace Infrastructure.Data.Repositories;

public class CsvCameraRepository(string csvPath) : ICameraRepository
{
    private List<Camera>? _cameras;

    public IEnumerable<Camera> LoadCsv()
    {
        if (_cameras != null && _cameras.Count > 0)
            return _cameras;

        try
        {
            using var reader = new StreamReader(csvPath);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<Camera>().ToList();

            _cameras = records;
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

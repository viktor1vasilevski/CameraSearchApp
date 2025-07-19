namespace Infrastructure.Data.Exceptions;

public class DataAccessException : Exception
{
    public DataAccessException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}

public class CsvParseException : DataAccessException
{
    public CsvParseException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}

public class DataLoadException : DataAccessException
{
    public DataLoadException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}

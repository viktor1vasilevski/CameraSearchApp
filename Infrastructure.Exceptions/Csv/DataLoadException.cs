namespace Infrastructure.Exceptions.Csv;

public class DataLoadException : Exception
{
    public DataLoadException(string message, Exception? inner = null)
        : base(message, inner)
    {
    }
}

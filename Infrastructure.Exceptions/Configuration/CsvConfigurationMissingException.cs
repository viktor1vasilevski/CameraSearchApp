namespace Infrastructure.Exceptions.Configuration
{
    public class CsvConfigurationMissingException : Exception
    {
        public CsvConfigurationMissingException(string message, Exception? inner = null)
            : base(message, inner)
        {
        }
    }
}

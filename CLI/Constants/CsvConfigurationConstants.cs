namespace Search.Constants;

public static class CsvConfigurationConstants
{
    public const string CsvPathMissingLog = "CSV path configuration (CsvSettings:CsvPath) is missing. Application cannot continue.";
    public const string CsvPathMissingUser = "Configuration error: CSV path is missing or invalid. Please check your appsettings or environment variables.";

    public const string UnhandledExceptionLog = "Unhandled exception occurred in Program.Main.";
    public const string UnhandledExceptionUser = "A fatal error occurred. Please check the logs for details.";
}

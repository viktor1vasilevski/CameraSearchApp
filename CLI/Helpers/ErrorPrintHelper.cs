namespace Search.Helpers;

public static class ErrorPrintHelper
{
    public static void PrintErrorMessage(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ResetColor();
    }
}

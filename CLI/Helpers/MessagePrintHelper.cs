namespace Search.Helpers;

public static class MessagePrintHelper
{
    public static void PrintMessage(string message, ConsoleColor color = ConsoleColor.Red)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

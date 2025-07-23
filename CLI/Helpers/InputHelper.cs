using Search.Extensions;

namespace SearchConsole.Helpers;

public static class InputHelper
{
    public static string GetNameOrPrompt(string[] args)
    {
        var nameFilter = args.GetNameArgument();

        return !string.IsNullOrWhiteSpace(nameFilter)
            ? nameFilter
            : PromptUntilValidName();
    }

    private static string PromptUntilValidName()
    {
        var name = PromptForName();

        while (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("You must enter a search term.");
            Console.WriteLine();
            name = PromptForName();
        }

        return name!;
    }

    private static string? PromptForName()
    {
        Console.Write("Please enter a name: ");
        return Console.ReadLine();
    }
}

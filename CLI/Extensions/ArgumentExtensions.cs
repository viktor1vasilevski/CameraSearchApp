namespace Search.Extensions;

public static class ArgumentExtensions
{
    public static string? GetNameArgument(this string[] args)
    {
        var argsDict = args
            .Select((arg, i) => new { arg, i })
            .Where(x => x.arg.StartsWith("--"))
            .ToDictionary(
                x => x.arg.TrimStart('-').ToLower(),
                x => args.Length > x.i + 1 ? args[x.i + 1] : ""
            );

        if (!argsDict.TryGetValue("name", out var nameFilter) || string.IsNullOrWhiteSpace(nameFilter))
        {
            Console.Write("Enter search term: ");
            nameFilter = Console.ReadLine();
        }

        return nameFilter;
    }
}

namespace Lieve.Crawler.Application.Helpers;

public static class ConsoleHelper
{
    public static void CustomConsoleWrite(string text, ConsoleColor consoleColor)
    {
        System.Console.ForegroundColor = consoleColor;
        System.Console.WriteLine(text);
        System.Console.ResetColor();
    }
}
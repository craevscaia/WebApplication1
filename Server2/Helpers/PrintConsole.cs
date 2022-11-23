namespace Server.Helpers;

public class PrintConsole
{
    public static void Write(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
    }
}
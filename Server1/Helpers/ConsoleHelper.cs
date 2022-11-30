namespace ClientServer.Helpers;

public static class ConsoleHelper
{
    public static void Print(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
    } 
}
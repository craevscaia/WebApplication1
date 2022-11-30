namespace Server.Helpers;

public static class IdGenerator
{
    private static readonly Mutex Mutex = new();
    private static int _id = 0;

    public static int GenerateId()
    {
        Mutex.WaitOne();
        _id++;
        Mutex.ReleaseMutex();
        return _id;
    }
}
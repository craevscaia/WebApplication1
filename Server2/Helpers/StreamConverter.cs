using System.Text;

namespace Server.Helpers;

public static class StreamConverter
{
    private static readonly Encoding Encoding = Encoding.UTF8;

    public static byte[] MessageToByteArray(string message)
    {
        var messageBytes = Encoding.GetBytes(message);
        var messageSize = messageBytes.Length;
        var completeSize = messageSize + 4;
        var completeMessage = new byte[completeSize];

        var sizeBytes = BitConverter.GetBytes(messageSize);
        sizeBytes.CopyTo(completeMessage, 0);
        messageBytes.CopyTo(completeMessage, 4);
        return completeMessage;
    }

    public static string StreamToMessage(Stream stream)
    {
        var sizeBytes = new byte[4];
        stream.Read(sizeBytes, 0, 4);
        var messageSize = BitConverter.ToInt32(sizeBytes, 0);
        var messageBytes = new byte[messageSize];
        stream.Read(messageBytes, 0, messageSize);
        var message = Encoding.GetString(messageBytes);
        string result = null!;

        foreach (var c in message)
            if (c != '\0')
            {
                result += c;
            }

        return result;
    }
}
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Server.Helpers;
using Server.Models;
using Server.Setting;
using IDataStorageService = Server.Services.DataService.IDataStorageService;

namespace Server.Services.TcpService;

public class TcpService : ITcpService
{
     private readonly IDataStorageService _dataService;

    public TcpService(IDataStorageService dataService)
    {
        _dataService = dataService;
    }

    #region Run Server 8081
    public async Task RunTcp()
    {
        Console.WriteLine("Server starting !");
 
        // IP Address to listen on. Loopback in this case
        var ipAddr = IPAddress.Loopback;
        // Port to listen on
        var port = Settings.Server1TcpSavePort;
        // Create a network endpoint
        var ep = new IPEndPoint(ipAddr, port);
        // Create and start a TCP listener
        var listener = new TcpListener(ep);
        listener.Start();
 
        Console.WriteLine("Server listening on: {0}:{1}", ep.Address, ep.Port);
 
        // keep running
        while(true)
        {
            var sender = await listener.AcceptTcpClientAsync();
            // streamToMessage - discussed later
            var request = StreamConverter.StreamToMessage(sender.GetStream());
            if (request != null)
            {
                var responseMessage = await MessageHandler(request);
                SendMessage(responseMessage, sender);
            }
        }
    }

    private async Task<string> MessageHandler(string message)
    {
        Console.WriteLine("Received message: " + message);
        var deserialized = JsonConvert.DeserializeObject<Data>(message);

        var resultSummary = await _dataService.Save(deserialized);
        
        var requestMessage = JsonConvert.SerializeObject(resultSummary);

        Console.WriteLine(deserialized?.FileName);
        Console.WriteLine(resultSummary.StorageCount);
        return requestMessage;
    }
    
    private void SendMessage(string message, TcpClient client)
    {
        var bytes = StreamConverter.MessageToByteArray(message);
        client.GetStream().Write(bytes, 0, bytes.Length);
    }
    #endregion

    #region SaveTo 8082 Server 2

    public Result? TcpSave(Data data, int serverPort)
    {
        var requestMessage = JsonConvert.SerializeObject(data);
        var responseMessage = SendMessage(requestMessage, serverPort);
        
        var deserialized = JsonConvert.DeserializeObject<Result>(responseMessage);

        Console.WriteLine(responseMessage);
        
        return deserialized;
    }

    private string SendMessage(string message, int serverPort)
    {
        var response = "";
        try
        {
            var client = new TcpClient("127.0.0.1", serverPort);
            client.NoDelay = true;
            var messageBytes = StreamConverter.MessageToByteArray(message);

            using (var stream = client.GetStream())
            {
                stream.Write(messageBytes, 0, messageBytes.Length);

                // Message sent!  Wait for the response stream of bytes...
                response = StreamConverter.StreamToMessage(stream);
            }

            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return response;
    }
    
    #endregion
}
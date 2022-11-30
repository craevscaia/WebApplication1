namespace Server.Models;

public class Data : Entity
{
    public int Id { get; init; }
    public string StreamData { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }
}   
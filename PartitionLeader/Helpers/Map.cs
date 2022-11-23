using PartitionLeader.Models;

namespace PartitionLeader.Helpers;

public static class Map
{
    public static Data MapData(this UploadedFile data)
    {
        var fileData = data.File;
        return new Data
        {
            Id = IdGenerator.GenerateId(),
            ContentType = fileData.ContentType,
            FileName = fileData.Name,
            StreamData = ConvertToByte(fileData)
        };
    }

    private static string ConvertToByte(IFormFile file)
    {
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        var fileBytes = ms.ToArray();
        var s = Convert.ToBase64String(fileBytes);
        return s;
    }
}
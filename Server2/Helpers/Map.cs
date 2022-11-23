
using Server.Models;

namespace Server.Helpers;
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
            // StreamData = fileData.OpenReadStream()
        };
    }
}
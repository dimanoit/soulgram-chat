using Soulgram.Chat.Domain.Models;
using FileInfo = Soulgram.File.Manager.Models.FileInfo;

namespace Soulgram.Chat.FileManagement;

public static class Convertor
{
    public static FileInfo ToFileInfo(this FileInfoDto fileInfo)
    {
        return new FileInfo
        {
            Content = fileInfo.Content,
            Name = fileInfo.Name,
            ContentType = fileInfo.ContentType
        };
    }
}
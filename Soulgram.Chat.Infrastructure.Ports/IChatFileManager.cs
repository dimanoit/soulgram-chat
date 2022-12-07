using Soulgram.Chat.Domain.Models;

namespace Soulgram.Chat.Infrastructure.Ports;

public interface IChatFileManager
{
    Task DeleteFileAsync(string fileUrl);

    Task<string> UploadFileAsync(FileInfoDto file, string userId);

    Task<IEnumerable<string>> UploadFilesAndGetIds(
        IEnumerable<FileInfoDto>? files,
        string userId);
}
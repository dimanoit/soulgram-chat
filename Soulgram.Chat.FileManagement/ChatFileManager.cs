using Microsoft.Extensions.Options;
using Soulgram.Chat.Domain.Models;
using Soulgram.Chat.Infrastructure.Ports;
using Soulgram.File.Manager;
using Soulgram.File.Manager.Interfaces;
using Soulgram.File.Manager.Models;
using FileInfo = Soulgram.File.Manager.Models.FileInfo;

namespace Soulgram.Chat.FileManagement;

public class ChatFileManager : FileManager, IChatFileManager
{
    public ChatFileManager(
        IOptions<BlobStorageOptions> storageOptions,
        IContainerNameResolver containerNameResolver)
        : base(storageOptions, containerNameResolver)
    {
    }

    public async Task<string> UploadFileAsync(FileInfoDto file, string userId)
    {
        return await base.UploadFileAsync(file.ToFileInfo(), userId);
    }

    public async Task<IEnumerable<string>> UploadFilesAndGetIds(IEnumerable<FileInfoDto>? files, string userId)
    {
        var filesInfos = files == null
            ? Array.Empty<FileInfo>()
            : files.Select(f => f.ToFileInfo());

        return await base.UploadFilesAndGetIds(filesInfos, userId);
    }
}
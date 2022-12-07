namespace Soulgram.Chat.Domain.Models;

public class FileInfoDto
{
    public string Name { get; init; } = null!;

    public string ContentType { get; init; } = null!;

    public Stream Content { get; init; } = null!;
}
using Soulgram.Chat.Domain.Enums;

namespace Soulgram.Chat.Domain.Entities;

public record AttachmentEntity
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = null!;
    public string ResourceLink { get; init; } = null!;

    public AttachmentType AttachmentType { get; init; }
}
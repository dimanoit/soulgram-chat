using Soulgram.Chat.Domain.Enums;

namespace Soulgram.Chat.Domain.Entities;

public class AttachmentEntity
{
    public string Name { get; init; } = null!;
    public string ResourceLink { get; init; } = null!;

    public AttachmentType AttachmentType { get; init; }
}
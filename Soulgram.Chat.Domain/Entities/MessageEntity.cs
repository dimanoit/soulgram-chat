namespace Soulgram.Chat.Domain.Entities;

public record MessageEntity
{
    public string Id { get; init; } = string.Empty;
    public string? Text { get; init; }
    public ICollection<AttachmentEntity>? Attachments { get; init; }
}
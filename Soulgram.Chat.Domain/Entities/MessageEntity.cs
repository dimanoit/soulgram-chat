namespace Soulgram.Chat.Domain.Entities;

public class MessageEntity
{
    public string? Id { get; init; }
    public string SenderId { get; init; } = null!;
    public string? Text { get; init; }
    public ICollection<AttachmentEntity>? Attachments { get; init; }
}
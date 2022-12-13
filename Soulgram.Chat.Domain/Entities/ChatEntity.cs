using Soulgram.Chat.Domain.Enums;

namespace Soulgram.Chat.Domain.Entities;

public class ChatEntity
{
    public string? Id { get; init; }
    public string? Title { get; set; }
    public ChatType ChatType { get; init; }
    public ICollection<string> ParticipantsIds { get; init; } = null!;
    public ICollection<string> AdminsIds { get; set; } = null!;

    public ICollection<MessageEntity> Messages { get; } = Array.Empty<MessageEntity>();
}
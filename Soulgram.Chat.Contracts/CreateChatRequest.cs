using Soulgram.Chat.Domain.Enums;

namespace Soulgram.Chat.Contracts;

public record CreateChatRequest
{
    public string InitiatorId { get; init; } = null!;
    public ChatType ChatType { get; init; }
    public string? Title { get; init; }
    public ICollection<string> ParticipantsIds { get; init; } = null!;
}
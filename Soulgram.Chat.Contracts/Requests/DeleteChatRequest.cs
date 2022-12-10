namespace Soulgram.Chat.Contracts.Requests;

public record DeleteChatRequest
{
    public string UserId { get; init; } = null!;
    public string ChatId { get; init; } = null!;
}
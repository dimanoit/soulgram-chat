namespace Soulgram.Chat.Contracts.Requests;

public record DeleteMessageRequest
{
    public string UserId { get; init; } = null!;
    public string ChatId { get; init; } = null!;
    public string MessageId { get; init; } = null!;
}
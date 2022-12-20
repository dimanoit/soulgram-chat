using Soulgram.Chat.Domain.Models;

namespace Soulgram.Chat.Contracts.Requests;

public record CreateMessageRequestDto
{
    public string SenderId { get; init; } = null!;
    public string ChatId { get; init; } = null!;

    public string? Text { get; init; }
    public IEnumerable<FileInfoDto>? Files { get; init; }
}
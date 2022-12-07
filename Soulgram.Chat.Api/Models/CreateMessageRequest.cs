using Soulgram.Chat.Contracts.Requests;
using Soulgram.Chat.Domain.Models;

namespace Soulgram.Chat.Api.Models;

public class CreateMessageRequest
{
    public string SenderId { get; init; } = null!;
    public string ChatId { get; init; } = null!;

    public string? Text { get; init; }
    public IFormFile[]? Files { get; init; }
}

public static class Converter
{
    public static CreateMessageRequestDto ToCreateMessageRequestDto(this CreateMessageRequest request)
    {
        var files = request.Files?.Select(f => f.ToFileInfo());

        return new CreateMessageRequestDto
        {
            Files = files,
            Text = request.Text,
            ChatId = request.ChatId,
            SenderId = request.SenderId
        };
    }

    private static FileInfoDto ToFileInfo(this IFormFile file)
    {
        var fileInfo = new FileInfoDto
        {
            Content = file.OpenReadStream(),
            ContentType = file.ContentType,
            Name = file.FileName
        };

        return fileInfo;
    }
}
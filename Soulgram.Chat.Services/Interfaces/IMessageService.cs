using Soulgram.Chat.Contracts.Requests;

namespace Soulgram.Chat.Services.Interfaces;

public interface IMessageService
{
    Task SendMessageAsync(CreateMessageRequestDto requestDto, CancellationToken cancellationToken);
}
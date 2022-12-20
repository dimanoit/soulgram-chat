using LanguageExt.Common;
using Soulgram.Chat.Contracts.Requests;

namespace Soulgram.Chat.Services.Interfaces;

public interface IMessageService
{
    Task<Result<bool>> SendMessageAsync(CreateMessageRequestDto requestDto, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteMessageAsync(DeleteMessageRequest request, CancellationToken cancellationToken);
}
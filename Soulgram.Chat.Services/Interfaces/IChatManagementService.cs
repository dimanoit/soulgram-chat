using LanguageExt.Common;
using Soulgram.Chat.Contracts.Requests;

namespace Soulgram.Chat.Services.Interfaces;

public interface IChatManagementService
{
    Task<Result<bool>> CreateChatAsync(CreateChatRequest request, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteChatAsync(DeleteChatRequest request, CancellationToken cancellationToken);
}
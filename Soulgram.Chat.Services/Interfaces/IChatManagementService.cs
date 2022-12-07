using Soulgram.Chat.Contracts;

namespace Soulgram.Chat.Services.Interfaces;

public interface IChatManagementService
{
    Task CreateChatAsync(CreateChatRequest request, CancellationToken cancellationToken);
}
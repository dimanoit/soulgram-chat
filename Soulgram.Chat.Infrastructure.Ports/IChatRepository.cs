using Soulgram.Chat.Domain.Entities;

namespace Soulgram.Chat.Infrastructure.Ports;

public interface IChatRepository
{
    Task AddMessageAsync(string chatId, MessageEntity message);
    
    Task DeleteMessageAsync(
        string chatId,
        string messageId);

    Task<MessageEntity> GetMessageAsync(
        string chatId,
        string messageId,
        CancellationToken cancellationToken);
}
using Soulgram.Chat.Domain.Entities;

namespace Soulgram.Chat.Infrastructure.Ports;

public interface IChatRepository
{
    Task AddMessage(string chatId, MessageEntity message);
}
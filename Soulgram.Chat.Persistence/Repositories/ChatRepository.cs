using MongoDB.Driver;
using Soulgram.Chat.Domain.Entities;
using Soulgram.Chat.Infrastructure.Ports;
using Soulgram.Mongo.Repository.Interfaces;

namespace Soulgram.Chat.Persistence.Repositories;

public class ChatRepository : GenericRepository<ChatEntity>, IChatRepository
{
    public ChatRepository(IMongoConnection connection)
        : base(connection)
    {
    }

    public async Task AddMessageAsync(string chatId, MessageEntity message)
    {
        var update = Builders<ChatEntity>
            .Update
            .Push(chatEntity => chatEntity.Messages, message);

        await Collection.FindOneAndUpdateAsync(g => g.Id == chatId, update);
    }

    public async Task DeleteMessageAsync(string chatId, string messageId)
    {
        var update = Builders<ChatEntity>
            .Update
            .PullFilter(chat => chat.Messages, message => message.Id == messageId);

        await Collection.FindOneAndUpdateAsync(g => g.Id == chatId, update);
    }

    public async Task<MessageEntity> GetMessageAsync(
        string chatId,
        string messageId,
        CancellationToken cancellationToken)
    {
        // _cache.TryGetValue(message)

        var message = await FindOneAsync(
            c => c.Id == chatId,
            c => c.Messages.First(m => m.Id == messageId),
            cancellationToken
        );

        return message!;
    }
}
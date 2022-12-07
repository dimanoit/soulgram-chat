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

    public async Task AddMessage(string chatId, MessageEntity message)
    {
        var update = Builders<ChatEntity>.Update
            .Push(chatEntity => chatEntity.Messages, message);

        await Collection.FindOneAndUpdateAsync(g => g.Id == chatId, update);
    }
}
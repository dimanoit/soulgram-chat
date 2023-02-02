using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using Soulgram.Chat.Domain.Entities;
using Soulgram.Chat.Infrastructure.Ports;
using Soulgram.Mongo.Repository.Interfaces;

namespace Soulgram.Chat.Persistence.Repositories;

public class ChatRepository : GenericRepository<ChatEntity>, IChatRepository
{
    private const int DefaultCacheSeconds = 2;
    private readonly IMemoryCache _memoryCache;

    public ChatRepository(
        IMongoConnection connection,
        IMemoryCache memoryCache)
        : base(connection)
    {
        _memoryCache = memoryCache;
    }

    public async Task AddMessageAsync(string chatId, MessageEntity message)
    {
        var update = Builders<ChatEntity>
            .Update
            .Push(chatEntity => chatEntity.Messages, message);

        await Collection.FindOneAndUpdateAsync(g => g.Id == chatId, update);
    }

    public async Task SetAdminList(string chatId, string[] adminsIds)
    {
        var update = Builders<ChatEntity>
            .Update
            .Set(ce => ce.AdminsIds, adminsIds);

        await Collection.FindOneAndUpdateAsync(g => g.Id == chatId, update);
    }

    public async Task DeleteMessageAsync(string chatId, string messageId)
    {
        var update = Builders<ChatEntity>
            .Update
            .PullFilter(chat => chat.Messages, message => message.Id == messageId);

        await Collection.FindOneAndUpdateAsync(g => g.Id == chatId, update);
    }

    public async Task<MessageEntity?> GetMessageAsync(
        string chatId,
        string messageId,
        CancellationToken cancellationToken)
    {
        var value = await _memoryCache.GetOrCreateAsync(chatId + messageId, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(DefaultCacheSeconds);
            return await FindOneAsync(
                c => c.Id == chatId,
                c => c.Messages.FirstOrDefault(m => m.Id == messageId),
                cancellationToken
            );
        });

        return value;
    }
}
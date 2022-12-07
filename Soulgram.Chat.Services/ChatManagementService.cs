using Soulgram.Chat.Contracts;
using Soulgram.Chat.Domain.Entities;
using Soulgram.Chat.Persistence.Ports;
using Soulgram.Chat.Services.Converters;
using Soulgram.Chat.Services.Interfaces;

namespace Soulgram.Chat.Services;

public class ChatManagementService : IChatManagementService
{
    private readonly IRepository<ChatEntity> _repository;

    public ChatManagementService(IRepository<ChatEntity> repository)
    {
        _repository = repository;
    }

    public async Task CreateChatAsync(CreateChatRequest request, CancellationToken cancellationToken)
    {
        var chatEntity = request.ToChatEntity();
        await _repository.InsertOneAsync(chatEntity, cancellationToken);
    }
}
using Soulgram.Chat.Contracts.Requests;
using Soulgram.Chat.Domain.Entities;
using Soulgram.Chat.Domain.Enums;

namespace Soulgram.Chat.Services.Converters;

public static class ChatDataConvertor
{
    public static ChatEntity ToChatEntity(this CreateChatRequest request)
    {
        var chatEntity = new ChatEntity
        {
            ParticipantsIds = request.ParticipantsIds,
            ChatType = request.ChatType
        };

        switch (request.ChatType)
        {
            case ChatType.Channel:
            case ChatType.Group:
                chatEntity.AdminsIds = new[] { request.InitiatorId };
                break;

            case ChatType.Dialog:
            case ChatType.SecretChat:
                chatEntity.AdminsIds = request.ParticipantsIds;
                break;
        }

        return chatEntity;
    }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Soulgram.Chat.Domain.Entities;
using Soulgram.Chat.Domain.Enums;
using Soulgram.Mongo.Repository.Interfaces;

namespace Soulgram.Chat.Persistence.ClassMappers;

public class ChatEntityMapper : IModelMapper
{
    public void MapFields()
    {
        BsonClassMap.RegisterClassMap<ChatEntity>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(chatEntity => chatEntity.Id)
                .SetIdGenerator(new StringObjectIdGenerator())
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            cm.MapMember(chatEntity => chatEntity.ChatType)
                .SetSerializer(new EnumSerializer<ChatType>(BsonType.String));

            cm.MapMember(chatEntity => chatEntity.Messages).SetIsRequired(true);
            cm.MapMember(chatEntity => chatEntity.AdminsIds).SetIsRequired(true);
            cm.MapMember(chatEntity => chatEntity.ParticipantsIds).SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<MessageEntity>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(messageEntity => messageEntity.Id)
                .SetIdGenerator(new StringObjectIdGenerator())
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            cm.MapMember(messageEntity => messageEntity.Text).SetIsRequired(true);
            cm.MapMember(messageEntity => messageEntity.SenderId).SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<AttachmentEntity>(cm =>
        {
            cm.AutoMap();

            cm.MapMember(attachmentEntity => attachmentEntity.Name).SetIsRequired(true);
            cm.MapMember(attachmentEntity => attachmentEntity.ResourceLink).SetIsRequired(true);
            cm.MapMember(attachmentEntity => attachmentEntity.AttachmentType)
                .SetSerializer(new EnumSerializer<AttachmentType>(BsonType.String))
                .SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });
    }
}
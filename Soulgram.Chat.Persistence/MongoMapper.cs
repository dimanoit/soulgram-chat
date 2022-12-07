using Soulgram.Chat.Persistence.ClassMappers;
using Soulgram.Mongo.Repository;
using Soulgram.Mongo.Repository.Interfaces;

namespace Soulgram.Chat.Persistence;

public class MongoMapper : FieldMapperBase
{
    public override IEnumerable<IModelMapper> GetModelMappers()
    {
        return new[] { new ChatEntityMapper() };
    }
}
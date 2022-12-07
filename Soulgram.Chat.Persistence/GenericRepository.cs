using Soulgram.Chat.Persistence.Ports;
using Soulgram.Mongo.Repository;
using Soulgram.Mongo.Repository.Interfaces;

namespace Soulgram.Chat.Persistence;

public class GenericRepository<TDocument> : MongoRepository<TDocument>,
    IRepository<TDocument> where TDocument : class
{
    public GenericRepository(IMongoConnection connection)
        : base(connection)
    {
    }
}
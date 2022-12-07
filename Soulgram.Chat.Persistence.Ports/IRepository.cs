using System.Linq.Expressions;

namespace Soulgram.Chat.Persistence.Ports;

public interface IRepository<TEntity> where TEntity : class
{
    Task<ICollection<TProjected>> FilterByAsync<TProjected>(
        Expression<Func<TEntity, bool>> filterExpression,
        Expression<Func<TEntity, TProjected>> projectionExpression,
        CancellationToken cancellationToken = default);

    Task<TProjected?> FindOneAsync<TProjected>(
        Expression<Func<TEntity, bool>> filterExpression,
        Expression<Func<TEntity, TProjected>> projectionExpression,
        CancellationToken cancellationToken = default);

    Task InsertOneAsync(TEntity document, CancellationToken cancellationToken = default);
    Task InsertManyAsync(ICollection<TEntity> documents, CancellationToken cancellationToken = default);
    Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression);
    Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression);
}
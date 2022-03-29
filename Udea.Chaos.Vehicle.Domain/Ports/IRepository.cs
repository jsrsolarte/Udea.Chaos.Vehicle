using Udea.Chaos.Vehicle.Domain.Entities;

namespace Udea.Chaos.Vehicle.Domain.Ports
{
    public interface IRepository<T, TId> : IReadRepository<T, TId>
       where T : EntityBase<TId>
    {
        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    }
}
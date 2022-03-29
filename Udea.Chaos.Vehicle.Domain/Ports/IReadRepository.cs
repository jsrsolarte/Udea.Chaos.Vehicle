using Ardalis.Specification;
using Udea.Chaos.Vehicle.Domain.Entities;

namespace Udea.Chaos.Vehicle.Domain.Ports
{
    public interface IReadRepository<T, TId>
        where T : EntityBase<TId>
    {
        Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

        Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

        Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

        Task<IEnumerable<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
}
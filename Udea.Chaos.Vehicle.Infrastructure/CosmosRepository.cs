using Ardalis.Specification;
using Microsoft.Azure.Cosmos;
using Udea.Chaos.Vehicle.Domain.Entities;
using Udea.Chaos.Vehicle.Domain.Ports;
using Udea.Chaos.Vehicle.Infrastructure.Specification;

namespace Udea.Chaos.Vehicle.Infrastructure
{
    public class CosmosRepository<T> : CosmosReadRepository<T>, IRepository<T, string>
        where T : EntityBase<string>
    {
        public CosmosRepository(ICosmosContainerFactory cosmosDbContainerFactory)
            : this(cosmosDbContainerFactory, SpecificationEvaluator.Default)
        { }

        public CosmosRepository(ICosmosContainerFactory cosmosDbContainerFactory, ISpecificationEvaluator specificationEvaluator)
            : base(cosmosDbContainerFactory, specificationEvaluator)
        { }

        public virtual Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (string.IsNullOrWhiteSpace(entity.Id))
            {
                typeof(EntityBase<string>).GetProperty("Id")?.SetValue(entity, GenerateId(entity), null);
            }

            return Container.CreateItemAsync(entity, ResolvePartitionKey(entity.Id), cancellationToken: cancellationToken);
        }

        public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            var tasks = entities.Select(_ => AddAsync(_));
            return Task.WhenAll(tasks);
        }

        public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Container.UpsertItemAsync(entity, ResolvePartitionKey(entity.Id), cancellationToken: cancellationToken);
        }

        public Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            var tasks = entities.Select(_ => UpdateAsync(_));
            return Task.WhenAll(tasks);
        }

        public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Container.DeleteItemAsync<T>(entity.Id, ResolvePartitionKey(entity.Id), cancellationToken: cancellationToken);
        }

        public Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            var tasks = entities.Select(_ => DeleteAsync(_));
            return Task.WhenAll(tasks);
        }
    }
}
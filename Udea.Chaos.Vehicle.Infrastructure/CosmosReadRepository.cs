using Ardalis.Specification;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Udea.Chaos.Vehicle.Domain.Entities;
using Udea.Chaos.Vehicle.Domain.Ports;
using Udea.Chaos.Vehicle.Infrastructure.Specification;

namespace Udea.Chaos.Vehicle.Infrastructure
{
    public class CosmosReadRepository<T> : IReadRepository<T, string>, IContainerContext<T>
        where T : EntityBase<string>
    {
        private readonly ISpecificationEvaluator _specificationEvaluator;

        private readonly CosmosLinqSerializerOptions LinqSerializerOptions = new()
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        };

        public CosmosReadRepository(ICosmosContainerFactory cosmosDbContainerFactory)
            : this(cosmosDbContainerFactory, SpecificationEvaluator.Default)
        { }

        public CosmosReadRepository(ICosmosContainerFactory cosmosDbContainerFactory, ISpecificationEvaluator specificationEvaluator)
        {
            if (cosmosDbContainerFactory is null)
            {
                throw new ArgumentNullException(nameof(cosmosDbContainerFactory));
            }

            Container = cosmosDbContainerFactory.GetContainer(ContainerName);
            _specificationEvaluator = specificationEvaluator ?? throw new ArgumentNullException(nameof(specificationEvaluator));
        }

        protected Container Container { get; }

        public virtual string ContainerName => typeof(T).Name;

        public virtual string GenerateId(T entity) => Guid.NewGuid().ToString();

        public virtual PartitionKey ResolvePartitionKey(string entityId) => new(entityId.Split(':')[0]);

        public virtual async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                ItemResponse<T> response = await Container.ReadItemAsync<T>(id, ResolvePartitionKey(id), cancellationToken: cancellationToken);
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public virtual async Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            var result = await ApplySpecification(specification).Take(1).ToFeedIterator().ReadNextAsync(cancellationToken);
            return result.SingleOrDefault();
        }

        public virtual async Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            var result = await ApplySpecification(specification).Take(1).ToFeedIterator().ReadNextAsync(cancellationToken);
            return result.SingleOrDefault();
        }

        public virtual async Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await Queryable().ToFeedIterator().ReadNextAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            IEnumerable<T> queryResult = await ApplySpecification(specification).ToFeedIterator().ReadNextAsync(cancellationToken);
            return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult);
        }

        public virtual async Task<IEnumerable<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            IEnumerable<TResult> queryResult = await ApplySpecification(specification).ToFeedIterator().ReadNextAsync(cancellationToken);
            return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult);
        }

        public virtual async Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).CountAsync(cancellationToken);
        }

        public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await Queryable().CountAsync(cancellationToken);
        }

        protected virtual IQueryable<T> ApplySpecification(ISpecification<T> specification, bool evaluateCriteriaOnly = false)
        {
            return _specificationEvaluator.GetQuery(Queryable(), specification, evaluateCriteriaOnly);
        }

        protected virtual IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
        {
            if (specification is null) throw new ArgumentNullException(nameof(specification));
            if (specification.Selector is null) throw new SelectorNotFoundException();

            return _specificationEvaluator.GetQuery(Queryable(), specification);
        }

        private IOrderedQueryable<T> Queryable() => Container.GetItemLinqQueryable<T>(allowSynchronousQueryExecution: true,
            linqSerializerOptions: LinqSerializerOptions);
    }
}
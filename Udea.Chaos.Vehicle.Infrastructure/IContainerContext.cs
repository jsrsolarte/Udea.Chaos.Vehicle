using Microsoft.Azure.Cosmos;
using Udea.Chaos.Vehicle.Domain.Entities;

namespace Udea.Chaos.Vehicle.Infrastructure
{
    public interface IContainerContext<T> where T : EntityBase<string>
    {
        string ContainerName { get; }

        string GenerateId(T entity);

        PartitionKey ResolvePartitionKey(string entityId);
    }
}
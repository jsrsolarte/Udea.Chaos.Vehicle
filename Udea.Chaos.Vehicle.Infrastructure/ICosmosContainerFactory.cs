using Microsoft.Azure.Cosmos;

namespace Udea.Chaos.Vehicle.Infrastructure
{
    public interface ICosmosContainerFactory
    {
        Container GetContainer(string containerName);
    }
}
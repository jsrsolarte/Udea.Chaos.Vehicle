using Microsoft.Azure.Cosmos;

namespace Udea.Chaos.Vehicle.Infrastructure
{
    public sealed class CosmosContainerFactory : ICosmosContainerFactory, IDisposable
    {
        private readonly CosmosClient _cosmosClient;

        private readonly string _databaseName;

        public CosmosContainerFactory(CosmosClient cosmosClient, string databaseName)
        {
            _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
            _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
        }

        public Container GetContainer(string containerName)
        {
            return _cosmosClient.GetContainer(_databaseName, containerName);
        }

        public void Dispose()
        {
            _cosmosClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
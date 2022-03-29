using Udea.Chaos.Vehicle.Domain.Ports;

namespace Udea.Chaos.Vehicle.Infrastructure.Adapters
{
    public class VehicleRepository : CosmosRepository<Domain.Entities.Vehicle>, IVechicleRepository
    {
        public VehicleRepository(ICosmosContainerFactory cosmosDbContainerFactory) : base(cosmosDbContainerFactory)
        {
        }
    }
}
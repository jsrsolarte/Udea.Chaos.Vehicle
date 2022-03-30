using Ardalis.Specification;

namespace Udea.Chaos.Vehicle.Domain.Specifications
{
    public class GetVehicleByOwnerIdSpec : Specification<Entities.Vehicle>
    {
        public GetVehicleByOwnerIdSpec(Guid ownerId)
        {
            Query.Where(_ => _.OwnerId == ownerId.ToString());
        }
    }
}
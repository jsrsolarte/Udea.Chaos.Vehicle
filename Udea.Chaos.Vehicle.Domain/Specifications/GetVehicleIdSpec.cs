using Ardalis.Specification;

namespace Udea.Chaos.Vehicle.Domain.Specifications
{
    public class GetVehicleIdSpec : Specification<Entities.Vehicle, string>
    {
        public GetVehicleIdSpec()
        {
            Query.Select(_ => _.Id);
        }
    }
}
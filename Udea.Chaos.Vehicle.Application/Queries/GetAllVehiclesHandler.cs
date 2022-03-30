using MediatR;
using Udea.Chaos.Vehicle.Domain.Ports;
using Udea.Chaos.Vehicle.Domain.Specifications;

namespace Udea.Chaos.Vehicle.Application.Queries
{
    public class GetAllVehiclesHandler : IRequestHandler<GetAllVehicles, IEnumerable<Guid>>
    {
        private readonly IVechicleRepository _vehicleRepository;

        public GetAllVehiclesHandler(IVechicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<Guid>> Handle(GetAllVehicles request, CancellationToken cancellationToken)
        {
            var spec = new GetVehicleIdSpec();
            var vehiclesIds = await _vehicleRepository.ListAsync(spec, cancellationToken);
            return vehiclesIds.Select(_ => Guid.Parse(_));
        }
    }
}
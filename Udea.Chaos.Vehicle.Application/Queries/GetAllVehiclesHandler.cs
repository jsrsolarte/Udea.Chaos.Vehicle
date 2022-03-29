using MediatR;
using Udea.Chaos.Vehicle.Application.Dtos;
using Udea.Chaos.Vehicle.Application.Extensions;
using Udea.Chaos.Vehicle.Domain.Ports;

namespace Udea.Chaos.Vehicle.Application.Queries
{
    public class GetAllVehiclesHandler : IRequestHandler<GetAllVehicles, IEnumerable<VehicleDto>>
    {
        private readonly IVechicleRepository _vehicleRepository;

        public GetAllVehiclesHandler(IVechicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<VehicleDto>> Handle(GetAllVehicles request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.ListAsync(cancellationToken);
            return vehicles.Select(_ => _.ToDto());
        }
    }
}
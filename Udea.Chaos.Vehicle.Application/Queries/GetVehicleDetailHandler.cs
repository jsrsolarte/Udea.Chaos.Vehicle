using MediatR;
using Udea.Chaos.Vehicle.Application.Dtos;
using Udea.Chaos.Vehicle.Application.Extensions;
using Udea.Chaos.Vehicle.Domain.Ports;

namespace Udea.Chaos.Vehicle.Application.Queries
{
    public class GetVehicleDetailHandler : IRequestHandler<GetVehicleDetail, VehicleDto?>
    {
        private readonly IVechicleRepository _vehicleRepository;

        public GetVehicleDetailHandler(IVechicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleDto?> Handle(GetVehicleDetail request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(request.Id.ToString(), cancellationToken);
            if (vehicle == null) return null;
            return vehicle.ToDto();
        }
    }
}
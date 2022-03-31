using MediatR;
using Udea.Chaos.Vehicle.Application.Dtos;
using Udea.Chaos.Vehicle.Application.Extensions;
using Udea.Chaos.Vehicle.Application.Ports;
using Udea.Chaos.Vehicle.Domain.Ports;

namespace Udea.Chaos.Vehicle.Application.Queries
{
    public class GetVehicleDetailHandler : IRequestHandler<GetVehicleDetail, VehicleWithJourneysDto?>
    {
        private readonly IVechicleRepository _vehicleRepository;
        private readonly IJourneyService _journeyService;

        public GetVehicleDetailHandler(IVechicleRepository vehicleRepository, IJourneyService journeyService)
        {
            _vehicleRepository = vehicleRepository;
            _journeyService = journeyService;
        }

        public async Task<VehicleWithJourneysDto?> Handle(GetVehicleDetail request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(request.Id.ToString(), cancellationToken);
            var journeys = await _journeyService.GetVehicles(request.Id);
            if (vehicle == null) return null;
            return vehicle.ToDto(journeys);
        }
    }
}
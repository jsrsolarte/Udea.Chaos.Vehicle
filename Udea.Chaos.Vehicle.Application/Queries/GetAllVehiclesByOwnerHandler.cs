using MediatR;
using Udea.Chaos.Vehicle.Application.Dtos;
using Udea.Chaos.Vehicle.Application.Extensions;
using Udea.Chaos.Vehicle.Domain.Ports;
using Udea.Chaos.Vehicle.Domain.Specifications;

namespace Udea.Chaos.Vehicle.Application.Queries
{
    public class GetAllVehiclesByOwnerHandler : IRequestHandler<GetAllVehiclesByOwner, IEnumerable<VehicleDto>>
    {
        private readonly IVechicleRepository _vechicleRepository;

        public GetAllVehiclesByOwnerHandler(IVechicleRepository vechicleRepository)
        {
            _vechicleRepository = vechicleRepository;
        }

        public async Task<IEnumerable<VehicleDto>> Handle(GetAllVehiclesByOwner request, CancellationToken cancellationToken)
        {
            var spec = new GetVehicleByOwnerIdSpec(request.ownerId);
            var vehicles = await _vechicleRepository.ListAsync(spec);

            return vehicles.Select(_ => _.ToDto());
        }
    }
}
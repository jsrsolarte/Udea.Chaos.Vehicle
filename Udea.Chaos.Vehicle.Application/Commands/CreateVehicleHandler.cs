using MediatR;
using Udea.Chaos.Vehicle.Application.Extensions;
using Udea.Chaos.Vehicle.Domain.Ports;

namespace Udea.Chaos.Vehicle.Application.Commands
{
    public class CreateVehicleHandler : AsyncRequestHandler<CreateVehicle>
    {
        private readonly IVechicleRepository _vehicleRepository;

        public CreateVehicleHandler(IVechicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        protected override async Task Handle(CreateVehicle request, CancellationToken cancellationToken)
        {
            await _vehicleRepository.AddAsync(request.ToEntity(), cancellationToken);
        }
    }
}
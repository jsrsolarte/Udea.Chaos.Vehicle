using MediatR;
using Udea.Chaos.Vehicle.Application.Dtos;

namespace Udea.Chaos.Vehicle.Application.Queries
{
    public record GetAllVehiclesByOwner(Guid OwnerId) : IRequest<IEnumerable<VehicleDto>>;
}
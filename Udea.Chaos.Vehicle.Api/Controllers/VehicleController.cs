using MediatR;
using Microsoft.AspNetCore.Mvc;
using Udea.Chaos.Vehicle.Application.Commands;
using Udea.Chaos.Vehicle.Application.Dtos;
using Udea.Chaos.Vehicle.Application.Queries;

namespace Udea.Chaos.Vehicle.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class VehicleController
    {
        private readonly IMediator _mediator;

        public VehicleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Guid>> GetVehicles()
        {
            return await _mediator.Send(new GetAllVehicles());
        }

        [HttpGet("detail/{id}")]
        public async Task<VehicleDto?> GetOwnerDetail(Guid id)
        {
            var owner = await _mediator.Send(new GetVehicleDetail(id));

            if (owner == null) return null;
            return owner;
        }

        [HttpGet("by-owner/{ownerId}")]
        public async Task<IEnumerable<VehicleDto>> GetVehiclesByOwner(Guid ownerId)
        {
            return await _mediator.Send(new GetAllVehiclesByOwner(ownerId));
        }

        [HttpPost]
        public async Task CreateVehicle(CreateVehicle createVehicle)
        {
            await _mediator.Send(createVehicle);
        }
    }
}
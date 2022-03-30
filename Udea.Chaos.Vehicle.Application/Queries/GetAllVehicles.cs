using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udea.Chaos.Vehicle.Application.Dtos;

namespace Udea.Chaos.Vehicle.Application.Queries
{
    public record GetAllVehicles(): IRequest<IEnumerable<Guid>>;
}

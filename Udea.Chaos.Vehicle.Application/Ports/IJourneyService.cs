using Udea.Chaos.Journey.Application.Dtos;

namespace Udea.Chaos.Vehicle.Application.Ports
{
    public interface IJourneyService
    {
        Task<IEnumerable<JourneyDto>> GetJourneys(Guid vehicleId);
    }
}
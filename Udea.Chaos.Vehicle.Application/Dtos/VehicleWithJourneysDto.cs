using Udea.Chaos.Journey.Application.Dtos;

namespace Udea.Chaos.Vehicle.Application.Dtos
{
    public record VehicleWithJourneysDto(
        string Id,
        string Plate,
        string Brand,
        string Model,
        string Type,
        string Vin,
        int Year,
        string OwnerId,
        IEnumerable<JourneyDto> Journeys
    ) : VehicleDto(Id, Plate, Brand, Model, Type, Vin, Year, OwnerId);
}
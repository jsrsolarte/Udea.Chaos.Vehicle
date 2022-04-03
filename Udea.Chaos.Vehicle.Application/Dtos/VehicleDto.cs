namespace Udea.Chaos.Vehicle.Application.Dtos
{
    public record VehicleDto(
        string Id,
        string Plate,
        string Brand,
        string Model,
        string Type,
        string Vin,
        int Year,
        string OwnerId
    );
}
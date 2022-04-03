using Udea.Chaos.Journey.Application.Dtos;
using Udea.Chaos.Vehicle.Application.Commands;
using Udea.Chaos.Vehicle.Application.Dtos;

namespace Udea.Chaos.Vehicle.Application.Extensions
{
    public static class DtosExtension
    {
        public static VehicleDto ToDto(this Domain.Entities.Vehicle vehicle)
        {
            return new VehicleDto(vehicle.Id, vehicle.Plate, vehicle.Brand, vehicle.Model, vehicle.Type, vehicle.Vin, vehicle.Year, vehicle.OwnerId);
        }

        public static VehicleWithJourneysDto ToDto(this Domain.Entities.Vehicle vehicle, IEnumerable<JourneyDto> journeys)
        {
            return new VehicleWithJourneysDto(vehicle.Id, vehicle.Plate, vehicle.Brand, vehicle.Model, vehicle.Type, vehicle.Vin, vehicle.Year, vehicle.OwnerId, journeys);
        }

        public static Domain.Entities.Vehicle ToEntity(this CreateVehicle vehicle)
        {
            return new Domain.Entities.Vehicle
            {
                Plate = vehicle.Plate,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Type = vehicle.Type,
                Vin = vehicle.Vin,
                Year = vehicle.Year,
                OwnerId = vehicle.OwnerId
            };
        }
    }
}
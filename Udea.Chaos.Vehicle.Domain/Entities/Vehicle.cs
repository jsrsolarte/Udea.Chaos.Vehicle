namespace Udea.Chaos.Vehicle.Domain.Entities
{
    public class Vehicle : EntityBase<string>
    {
        public string Plate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Vin { get; set; } = string.Empty;
        public int Year { get; set; }
        public string OwnerId { get; set; } = string.Empty;
    }
}
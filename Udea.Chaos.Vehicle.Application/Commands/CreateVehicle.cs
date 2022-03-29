using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Udea.Chaos.Vehicle.Application.Commands
{
    public class CreateVehicle : IRequest
    {
        [Required]
        public string Plate { get; set; } = string.Empty;

        [Required]
        public string Brand { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public string Vin { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }

        [Required]
        public string OwnerId { get; set; } = string.Empty;
    }
}
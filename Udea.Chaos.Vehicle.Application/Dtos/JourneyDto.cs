namespace Udea.Chaos.Journey.Application.Dtos
{
    public record JourneyDto(double Kilometers, double PricePerKilometer, DateTime InitialDateTime, DateTime FinalDateTime, string UserEmail);
}
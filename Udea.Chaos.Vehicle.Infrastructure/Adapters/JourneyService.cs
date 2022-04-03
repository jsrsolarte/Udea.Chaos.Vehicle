using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Configuration;
using Udea.Chaos.Journey.Application.Dtos;
using Udea.Chaos.Vehicle.Application.Ports;

namespace Udea.Chaos.Owner.Infrastructure.Adapters
{
    public class JourneyService : IJourneyService
    {
        private readonly IFlurlClient _flurlClient;

        public JourneyService(IFlurlClientFactory flurlClientFac, IConfiguration config)
        {
            _flurlClient = flurlClientFac.Get(config.GetValue<string>("UrlJourneyApi"));
        }

        public async Task<IEnumerable<JourneyDto>> GetJourneys(Guid vehicleId)
        {
            var response = await _flurlClient.Request($"by-vehicle/{vehicleId}").GetAsync();
            return await response.GetJsonAsync<IEnumerable<JourneyDto>>();
        }
    }
}
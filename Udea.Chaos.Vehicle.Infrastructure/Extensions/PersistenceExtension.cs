using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Udea.Chaos.Vehicle.Domain.Ports;
using Udea.Chaos.Vehicle.Infrastructure.Adapters;
using Udea.Chaos.Vehicle.Infrastructure.Serialization;

namespace Udea.Chaos.Vehicle.Infrastructure.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddCosmosPersistence(this IServiceCollection svc, string connectionString, string databaseName)
        {
            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter() },
                IgnoreReadOnlyFields = true
            };

            return svc.AddCosmosPersistence(connectionString, databaseName, jsonSerializerOptions);
        }

        public static IServiceCollection AddCosmosPersistence(this IServiceCollection svc,
            string connectionString,
            string databaseName,
            JsonSerializerOptions jsonSerializerOptions)
        {
            svc.TryAddSingleton(_ => new CosmosClient(connectionString, new CosmosClientOptions()
            {
                Serializer = new CosmosSystemTextJsonSerializer(jsonSerializerOptions)
            }));

            svc.AddSingleton<ICosmosContainerFactory>(_ => new CosmosContainerFactory(_.GetRequiredService<CosmosClient>(), databaseName));

            svc.AddHealthChecks().AddCosmosDb(connectionString, databaseName);

            return svc;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection svc, IConfiguration config)
        {
            var connectionString = config.GetValue<string>("CosmosSettings:ConnectionString");
            var databaseName = config.GetValue<string>("CosmosSettings:DatabaseName");

            svc.AddCosmosPersistence(connectionString, databaseName);

            svc.AddTransient<IVechicleRepository, VehicleRepository>();

            return svc;
        }
    }
}
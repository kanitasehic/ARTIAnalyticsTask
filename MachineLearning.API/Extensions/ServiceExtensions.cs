using MachineLearning.Core.Configuration;
using MachineLearning.Core.Interfaces;
using MachineLearning.Core.Interfaces.HttpClients;
using MachineLearning.Core.Services;
using MachineLearning.Infrastructure.HttpClients;

namespace MachineLearning.API.Extensions;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IForecastService, ForecastService>();
    }

    public static void RegisterClients(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddHttpClient<IMachineLearningClient, MachineLearningClient>(client =>
        {
            client.BaseAddress = new Uri(appSettings.MachineLearningApiUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return new MachineLearningClient(client);
        });
    }
}

using MachineLearning.Core.DTOs.Requests;
using MachineLearning.Core.DTOs.Responses;
using MachineLearning.Core.Interfaces;
using MachineLearning.Core.Interfaces.HttpClients;

namespace MachineLearning.Core.Services;

public class ForecastService : IForecastService
{
    private readonly IMachineLearningClient _machineLearningClient;

    public ForecastService(IMachineLearningClient machineLearningClient)
    {
        _machineLearningClient = machineLearningClient;
    }

    public async Task<GetRegressionForecastResponse> GetRegressionForecast(GetRegressionForecastRequest request)
    {
        return await _machineLearningClient.GetRegressionForecastAsync(request);
    }

    public async Task<GetTimeseriesForecastResponse> GetTimeseriesForecast(GetTimeseriesForecastRequest request)
    {
        return await _machineLearningClient.GetTimeseriesForecastAsync(request);
    }
}

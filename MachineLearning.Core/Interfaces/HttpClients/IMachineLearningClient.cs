using MachineLearning.Core.DTOs.Requests;
using MachineLearning.Core.DTOs.Responses;

namespace MachineLearning.Core.Interfaces.HttpClients;

public interface IMachineLearningClient
{
    Task<GetRegressionForecastResponse> GetRegressionForecastAsync(GetRegressionForecastRequest getRegressionForecastRequest);

    Task<GetTimeseriesForecastResponse> GetTimeseriesForecastAsync(GetTimeseriesForecastRequest getTimeseriesForecastRequest);
}

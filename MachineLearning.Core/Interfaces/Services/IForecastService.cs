using MachineLearning.Core.DTOs.Requests;
using MachineLearning.Core.DTOs.Responses;

namespace MachineLearning.Core.Interfaces;

public interface IForecastService
{
    Task<GetRegressionForecastResponse> GetRegressionForecast(GetRegressionForecastRequest request);

    Task<GetTimeseriesForecastResponse> GetTimeseriesForecast(GetTimeseriesForecastRequest request);
}

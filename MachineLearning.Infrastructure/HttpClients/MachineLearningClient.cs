using MachineLearning.Core.DTOs.Requests;
using MachineLearning.Core.DTOs.Responses;
using MachineLearning.Core.Exceptions;
using MachineLearning.Core.Interfaces.HttpClients;
using MachineLearning.Core.Utils;
using Newtonsoft.Json;
using System.Text;

namespace MachineLearning.Infrastructure.HttpClients;

public class MachineLearningClient : IMachineLearningClient
{
    private readonly HttpClient _httpClient;
    private const string REGRESSION_URI = "/forecast/regression";
    private const string TIMESERIES_URI = "/forecast/timeseries";

    public MachineLearningClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<GetRegressionForecastResponse> GetRegressionForecastAsync(GetRegressionForecastRequest getRegressionForecastRequest)
    {

        var requestBody = JsonConvert.SerializeObject(getRegressionForecastRequest);
        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(REGRESSION_URI, content);

        if (!response.IsSuccessStatusCode)
        {
            var error = response.Content.ReadAsStringAsync();
            throw new BadRequestException(error.Result);
        }

        return await response.DeserializeAsync<GetRegressionForecastResponse>();
    }

    public async Task<GetTimeseriesForecastResponse> GetTimeseriesForecastAsync(GetTimeseriesForecastRequest getTimeseriesForecastRequest)
    {
        var uri = $"{TIMESERIES_URI}";

        if (getTimeseriesForecastRequest.NumberOfPredictions is not null)
        {
            uri += $"?numberOfPredictions={getTimeseriesForecastRequest.NumberOfPredictions}";
        }

        var response = await _httpClient.GetAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            var error = response.Content.ReadAsStringAsync();
            throw new BadRequestException(error.Result);
        }

        return await response.DeserializeAsync<GetTimeseriesForecastResponse>();
    }
}

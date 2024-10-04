namespace MachineLearning.Core.DTOs.Responses;

public class GetTimeseriesForecastResponse
{
    public IEnumerable<decimal> Forecasts { get; set; }
}

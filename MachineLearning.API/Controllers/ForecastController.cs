using MachineLearning.Core.DTOs;
using MachineLearning.Core.DTOs.Requests;
using MachineLearning.Core.DTOs.Responses;
using MachineLearning.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace MachineLearning.API.Controllers;

[Route("forecast")]
[ApiController]
public class ForecastController : ControllerBase
{
    private readonly IForecastService _forecastService;

    public ForecastController(IForecastService forecastService)
    {
        _forecastService = forecastService;
    }

    /// <summary>
    /// Regression forecast.
    /// </summary>
    /// <param name="getRegressionForecast">Regression forecast request.</param>
    /// <returns>Regression forecast result.</returns>
    /// <response code="200">The request has been fulfilled and data has been forecasted.</response>
    /// <response code="400">Error caused by invalid or missing data.</response>
    /// <response code="500">Error caused by remote technical issues, this status code is used for unforeseen technical issues.</response>
    [ProducesResponseType(typeof(GetRegressionForecastResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [HttpPost("regression")]
    public async Task<IActionResult> GetRegressionForecast([FromBody] GetRegressionForecastRequest getRegressionForecast)
    {
        var regressionForecast = await _forecastService.GetRegressionForecast(getRegressionForecast);
        return Ok(regressionForecast);
    }

    /// <summary>
    /// Timeseries forecast.
    /// </summary>
    /// <param name="getTimeseriesForecastRequest">Timeseries forecast request.</param>
    /// <returns>Timeseries forecast result.</returns>
    /// <response code="200">The request has been fulfilled and data has been forecasted.</response>
    /// <response code="400">Error caused by invalid or missing data.</response>
    /// <response code="500">Error caused by remote technical issues, this status code is used for unforeseen technical issues.</response>
    [ProducesResponseType(typeof(GetTimeseriesForecastResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet("timeseries")]
    public async Task<IActionResult> GetTimeseriesForecast([FromQuery] GetTimeseriesForecastRequest getTimeseriesForecastRequest)
    {
        var timeseriesForecast = await _forecastService.GetTimeseriesForecast(getTimeseriesForecastRequest);
        return Ok(timeseriesForecast);
    }
}

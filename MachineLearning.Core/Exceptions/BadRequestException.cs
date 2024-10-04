using MachineLearning.Core.Constants;
using MachineLearning.Core.DTOs;

namespace MachineLearning.Core.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message)
    : base(message)
    {
    }

    public static explicit operator ErrorResponse(BadRequestException obj) => new ErrorResponse
    {
        Title = ErrorMessages.BAD_REQUEST_ERROR,
        Description = obj.Message
    };
}

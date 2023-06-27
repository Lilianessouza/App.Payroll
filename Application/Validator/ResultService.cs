using FluentValidation.Results;
using System.Net;

namespace Application.Validator;

public class ResultService
{
    public bool IsSucess { get; set; }
    public string Message { get; set; }
    public ICollection<ErrorMessage> Errors { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    

    public static ResultService CreateError(string message, ValidationResult validationResult)
    {
        return new ResultService
        {
            IsSucess = false,
            Message = message,
            Errors = validationResult.Errors.Select(x => new ErrorMessage { Field = x.PropertyName, Message = x.ErrorMessage }).ToList(),
        };
    }

    public static ResultService Fail(string message)
    {
        return new ResultService { IsSucess = false, Message = message, StatusCode = HttpStatusCode.BadRequest };
    }

    public static ResultService Ok(string message)
    {
        return new ResultService { IsSucess = true, Message = message };
    }

    public static ResultService NotFound(string result)
    {
        return new ResultService { IsSucess = false, Message = result,StatusCode = HttpStatusCode.NoContent  };
    }

    public static ResultService<T> CreateError<T>(string message, ValidationResult validationResult)
    {
        return new ResultService<T>
        {
            IsSucess = false,
            Message = message,
            Errors = validationResult.Errors.Select(x => new ErrorMessage { Field = x.PropertyName, Message = x.ErrorMessage }).ToList(),
        };
    }

    public static ResultService<T> Fail<T>(string message)
    {
        return new ResultService<T> { IsSucess = false, Message = message };
    }

    public static ResultService<T> Ok<T>(T result)
    {
        return new ResultService<T> { IsSucess = true, Result = result };
    }

    public static ResultService<T> NoContent<T>(string message)
    {
        return new ResultService<T> { IsSucess = false, Message = message, StatusCode = HttpStatusCode.NoContent };
    }
}

public class ResultService<T> : ResultService
{
    public T Result { get; set; }
}

public class ErrorMessage
{
    public string? Message { get; set; }
    public string? Field { get; set; }
}



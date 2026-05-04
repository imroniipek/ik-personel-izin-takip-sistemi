using System.Net;
using System.Text.Json;
using Refit;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace Shared.ServiceResult;

public class ServiceResult
{
    public HttpStatusCode StatusCode { get; set; }
    
    public ProblemDetails? Fail { get; set; }

    public bool IsSuccess => Fail is null;


    public static ServiceResult SuccessAsNoContent()
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.NoContent,
        };
    }

    public static ServiceResult ErrorAsNoFound()
    {
        return new ServiceResult
        {
            StatusCode = HttpStatusCode.NotFound,

            Fail = new ProblemDetails()
            {
                Title = "Not Found Error",
                Detail = "The requested resource was not found"
            }
        };
    }

    public static ServiceResult Error(string title, string description, HttpStatusCode httpStatusCode)
    {
        return new ServiceResult
        {
            StatusCode = httpStatusCode,
            Fail = new ProblemDetails()
            {
                Title = title,
                Detail = description,
            }
        };
    }

    public static ServiceResult ErrorFromRefit(ApiException apiException)
    {
        if (string.IsNullOrWhiteSpace(apiException.Content))
        {
            return new ServiceResult
            {
                StatusCode = apiException.StatusCode,
                Fail = new ProblemDetails
                {
                    Title = "Remote service error",
                    Detail = apiException.Message
                }
            };
        }

        try
        {
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(
                apiException.Content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return new ServiceResult
            {
                StatusCode = apiException.StatusCode,
                Fail = problemDetails ?? new ProblemDetails
                {
                    Title = "Remote service error",
                    Detail = apiException.Content
                }
            };
        }
        catch
        {
            return new ServiceResult
            {
                StatusCode = apiException.StatusCode,
                Fail = new ProblemDetails
                {
                    Title = "Remote service error",
                    Detail = apiException.Content
                }
            };
        }
    }
}

public class ServiceResult<T>:ServiceResult
{
    public T? Data { get; set; }
    
    public string? UrlAsCreated { get; set; }

    public static ServiceResult<T> SuccessOk(T data)
    {
        return new ServiceResult<T>
        {
            StatusCode = HttpStatusCode.OK,
            Data = data
        };
    }
    public static ServiceResult<T> SuccessCreatedOk(T data,string url)
    {
        return new ServiceResult<T>
        {
            StatusCode = HttpStatusCode.Created,
            UrlAsCreated = url,
            Data = data
        };
    }

    public new static ServiceResult<T> SuccessAsNoContent()
    {
        return new ServiceResult<T>()
        {
            StatusCode = HttpStatusCode.NoContent
        };
    }
    
    public  static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
    {
        Console.WriteLine(exception.Content);

        if (string.IsNullOrWhiteSpace(exception.Content))
        {
            return new ServiceResult<T>
            {
                StatusCode = exception.StatusCode,
                Fail = new ProblemDetails
                {
                    Title = "Remote service error",
                    Detail = exception.Message
                }
            };
        }

        try
        {
            var theProblemDetails = JsonSerializer.Deserialize<ProblemDetails>(
                exception.Content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return new ServiceResult<T>
            {
                StatusCode = exception.StatusCode,
                Fail = theProblemDetails ?? new ProblemDetails
                {
                    Title = "Remote service error",
                    Detail = exception.Content
                }
            };
        }
        catch
        {
            return new ServiceResult<T>
            {
                StatusCode = exception.StatusCode,
                Fail = new ProblemDetails
                {
                    Title = "Remote service error",
                    Detail = exception.Content
                }
            };
        }
    }
    
    public new static ServiceResult<T> Error(string title, string description, HttpStatusCode httpStatusCode)
    {
        return new ServiceResult<T>()
        {
            StatusCode = httpStatusCode,
            Fail = new ProblemDetails()
            {
                Title=title,
                Detail=description,
            }
        };
    }

    public  static ServiceResult<T> ErrorAsNotFound()
    {
        return new ServiceResult<T>()
        {
            StatusCode = HttpStatusCode.NotFound,

            Fail = new ProblemDetails()
            {
                Title = "Not Found Error",
                Detail = $"The {typeof(T).Name} was not found",
            }

        };
    }
    
    

    public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Validation Error Occured",
            Detail = "Please check these properties for more details"
        };

        foreach (var error in errors)
        {
            problemDetails.Extensions.Add(error.Key, error.Value);
        }

        return new ServiceResult
        {
            StatusCode = HttpStatusCode.BadRequest,
            Fail = problemDetails
        };
    }
    
}
using Microsoft.AspNetCore.Http;

namespace PickPoint.Test.Common.Enums;

public enum ErrorCode
{
    NoError = 0,
    UnDefined = 1,
    NotFound = 2,
    Unauthorized = 3,
    Forbid = 4,
    Validation = 5,
    BadRequest = 6
}

public static class ErrorCodeExtensions
{
    public static string GetMessage(this ErrorCode errorCode)
    {
        switch (errorCode)
        {
            case ErrorCode.NotFound:
                return "Не найдено";
            case ErrorCode.Unauthorized:
                return "Необходима авторизaция";
            case ErrorCode.Forbid:
                return "Доступ запрещен";
            case ErrorCode.Validation:
                return "Ошибка валидации";
            case ErrorCode.BadRequest:
                return "Некорректный запрос";
            case ErrorCode.UnDefined:
            default:
                return "Внутренняя ошибка";
        }
    }

    public static int ToHttpStatusCode(this ErrorCode errorCode)
    {
        switch (errorCode)
        {
            case ErrorCode.NoError:
                return StatusCodes.Status200OK;

            case ErrorCode.Validation:
            case ErrorCode.BadRequest:
                return StatusCodes.Status400BadRequest;

            case ErrorCode.NotFound:
                return StatusCodes.Status404NotFound;

            case ErrorCode.Unauthorized:
                return StatusCodes.Status401Unauthorized;

            case ErrorCode.Forbid:
                return StatusCodes.Status403Forbidden;
            case ErrorCode.UnDefined:
            default:
                return StatusCodes.Status500InternalServerError;
        }
    }
}

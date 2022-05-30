using Microsoft.AspNetCore.Mvc;
using PickPoint.Test.Common.DTOs;
using PickPoint.Test.Common.Enums;
using PickPoint.Test.Common.Models;

namespace PickPoint.Test.Common.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToHttpResponse(this Result result)
    {
        return result.IsSuccess
            ? new NoContentResult()
            : new JsonResult(new ErrorDTO(result.Message, result.ErrorCode)) { StatusCode = result.ErrorCode.ToHttpStatusCode() };
    }

    public static IActionResult ToHttpResponse<T>(this Result<T> result)
    {
        return result.IsSuccess
            ? new OkObjectResult(result.Data)
            : new JsonResult(new ErrorDTO(result.Message, result.ErrorCode)) { StatusCode = result.ErrorCode.ToHttpStatusCode() };
    }

    public static IActionResult ToHttpResponse<T>(this Result<T> result, Func<T, IActionResult> okResultFunc)
    {
        return result.IsSuccess
            ? okResultFunc(result.Data)
            : new JsonResult(new ErrorDTO(result.Message, result.ErrorCode)) { StatusCode = result.ErrorCode.ToHttpStatusCode() };
    }
}

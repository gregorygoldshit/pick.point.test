using PickPoint.Test.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Test.Common.DTOs;

/// <summary>
/// Ошибка
/// </summary>
public record ErrorDTO
{
    /// <summary>
    /// Заголовок ошибки
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Код ошибки
    /// </summary>
    public int Code { get; }

    /// <summary>
    /// Текст ошибки
    /// </summary>
    public string Message { get; }

    public ErrorDTO(string message)
    {
        Message = message;
        Code = (int)ErrorCode.UnDefined;
        Title = ErrorCode.UnDefined.GetMessage();
    }

    public ErrorDTO(string message, ErrorCode errorCode)
    {
        Message = message;
        Code = (int)errorCode;
        Title = errorCode.GetMessage();
    }
}

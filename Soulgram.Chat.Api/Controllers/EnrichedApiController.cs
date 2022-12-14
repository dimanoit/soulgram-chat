using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace Soulgram.Chat.Api.Controllers;

public class EnrichedApiController : ControllerBase
{
    protected IActionResult GetHandledResult(Result<bool> result)
    {
        return result.Match<IActionResult>(
            _ => Ok(),
            exception => exception is ValidationException validationException
                ? BadRequest(validationException.Errors)
                : StatusCode(StatusCodes.Status500InternalServerError));
    }
}
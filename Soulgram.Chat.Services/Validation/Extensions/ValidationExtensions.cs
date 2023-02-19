using FluentValidation;
using LanguageExt.Common;

namespace Soulgram.Chat.Services.Validation.Extensions;

public static class ValidationExtensions
{
    public static async Task<Result<bool>> ValidateWithResultAsync<T>(
        this IValidator<T> validator,
        T request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var validationException = new ValidationException(validationResult.Errors);
            return new Result<bool>(validationException);
        }

        return true;
    }
}
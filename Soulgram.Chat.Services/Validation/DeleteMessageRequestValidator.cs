using FluentValidation;
using Soulgram.Chat.Contracts.Requests;
using Soulgram.Chat.Infrastructure.Ports;
using Soulgram.Chat.Services.Validation.CustomValidators;

namespace Soulgram.Chat.Services.Validation;

public class DeleteMessageRequestValidator : AbstractValidator<DeleteMessageRequest>
{
    private readonly IChatRepository _repository;

    public DeleteMessageRequestValidator(IChatRepository repository)
    {
        _repository = repository;

        RuleFor(r => r.ChatId)
            .NotEmpty().NotNull();

        RuleFor(r => r.UserId)
            .NotEmpty().NotNull();

        RuleFor(r => r.UserId)
            .Must(CustomValidatorRules.IsGuid)
            .WithMessage("Value should be a guid");

        RuleFor(r => r)
            .MustAsync(async (r, cancellation) => await IsTheSenderTriesToDeleteMessage(r, cancellation))
            .WithMessage("User can delete only own messages");
    }

    private async Task<bool> IsTheSenderTriesToDeleteMessage(
        DeleteMessageRequest request,
        CancellationToken cancellationToken)
    {
        var message = await _repository.GetMessageAsync(
            request.ChatId,
            request.MessageId,
            cancellationToken);

        return message?.SenderId == request.UserId;
    }
}
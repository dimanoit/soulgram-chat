using FluentValidation;
using Soulgram.Chat.Contracts.Requests;
using Soulgram.Chat.Domain.Entities;
using Soulgram.Chat.Domain.Enums;
using Soulgram.Chat.Persistence.Ports;
using Soulgram.Chat.Services.Validation.Rules;

namespace Soulgram.Chat.Services.Validation.Validators;

public class CreateChatRequestValidator : AbstractValidator<CreateChatRequest>
{
    private readonly IRepository<ChatEntity> _repository;

    public CreateChatRequestValidator(IRepository<ChatEntity> repository)
    {
        _repository = repository;

        RuleFor(r => r.InitiatorId)
            .NotEmpty().NotNull();

        RuleFor(r => r.InitiatorId)
            .Must(CustomValidatorRules.IsGuid)
            .WithMessage("Value should be a guid");

        RuleFor(r => r.Title)
            .NotEmpty().NotNull()
            .When(r => r.ChatType is ChatType.Channel or ChatType.Group)
            .WithMessage($"Title should be not empty for {ChatType.Channel} or {ChatType.Group}");

        RuleFor(r => r.ParticipantsIds)
            .Must(x => x == null)
            .When(r => r.ChatType == ChatType.Channel)
            .WithMessage("Channel can't be created with participants");

        RuleFor(r => r.ParticipantsIds)
            .Must(CustomValidatorRules.IsArrayHasDuplicates)
            .When(r => r.ChatType != ChatType.Channel)
            .WithMessage("Each participant should have a own unique id");

        RuleFor(r => r.ParticipantsIds)
            .Must(x => x.All(CustomValidatorRules.IsGuid))
            .When(r => r.ChatType != ChatType.Channel)
            .WithMessage("Each participant should have a guid id");

        RuleFor(r => r)
            .MustAsync(async (r, cancellation) =>
                await IsDialogNotExistsAsync(r.InitiatorId, r.ChatType, cancellation))
            .WithMessage("Dialog with this participants already exists")
            .OverridePropertyName(x => x.ParticipantsIds);
    }

    private async Task<bool> IsDialogNotExistsAsync(
        string initiatorId,
        ChatType chatType,
        CancellationToken cancellationToken)
    {
        if (chatType != ChatType.Dialog) return true;

        var dialogId = await _repository.FindOneAsync(
            chatEntity => chatEntity.ChatType == ChatType.Dialog && chatEntity.AdminsIds.Contains(initiatorId),
            chatEntity => chatEntity.Id,
            cancellationToken);

        return string.IsNullOrEmpty(dialogId);
    }
}
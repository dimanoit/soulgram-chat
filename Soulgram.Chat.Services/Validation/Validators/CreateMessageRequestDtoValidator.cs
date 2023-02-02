using FluentValidation;
using Soulgram.Chat.Contracts.Requests;
using Soulgram.Chat.Domain.Entities;
using Soulgram.Chat.Domain.Enums;
using Soulgram.Chat.Persistence.Ports;

namespace Soulgram.Chat.Services.Validation.Validators;

public class CreateMessageRequestDtoValidator : AbstractValidator<CreateMessageRequestDto>
{
    private readonly IRepository<ChatEntity> _repository;

    public CreateMessageRequestDtoValidator(IRepository<ChatEntity> repository)
    {
        _repository = repository;

        RuleFor(r => r.Text)
            .NotEmpty().NotNull()
            .When(r => r.Files == null || r.Files.Length() == 0)
            .WithMessage("Text of message couldn't be empty");

        RuleFor(r => r.Files)
            .NotEmpty().NotNull()
            .When(r => string.IsNullOrEmpty(r.Text))
            .WithMessage("If message doesn't contain text it should contain file'");

        RuleFor(r => r)
            .MustAsync(async (r, cancellation) =>
                await CanUserSendMessageInThisChat(r.ChatId, r.SenderId, cancellation))
            .WithMessage("User don't have permission to send message in this channel");
    }

    private async Task<bool> CanUserSendMessageInThisChat(
        string chatId,
        string userId,
        CancellationToken cancellationToken)
    {
        var chat = await _repository.FindOneAsync(
            chatEntity => chatEntity.Id == chatId,
            chatEntity => new
            {
                chatEntity.AdminsIds,
                chatEntity.ParticipantsIds,
                chatEntity.ChatType
            }, cancellationToken);

        if (chat?.ChatType is ChatType.Dialog or ChatType.Group) return chat.ParticipantsIds.Contains(userId);

        if (chat?.ChatType == ChatType.Channel) return chat.AdminsIds.Contains(userId);

        return true;
    }
}
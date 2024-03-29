﻿using FluentValidation;
using Soulgram.Chat.Contracts.Requests;
using Soulgram.Chat.Domain.Entities;
using Soulgram.Chat.Persistence.Ports;
using Soulgram.Chat.Services.Validation.CustomValidators;

namespace Soulgram.Chat.Services.Validation;

public class DeleteChatRequestValidator : AbstractValidator<DeleteChatRequest>
{
    private readonly IRepository<ChatEntity> _repository;

    public DeleteChatRequestValidator(IRepository<ChatEntity> repository)
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
            .MustAsync(async (r, cancellation) =>
                await IsUserAdminOfChat(r.UserId, r.ChatId, cancellation))
            .WithMessage("User don't have permission to delete this chat");
    }

    private async Task<bool> IsUserAdminOfChat(
        string userId,
        string chatId,
        CancellationToken cancellationToken)
    {
        var chat = await _repository.FindOneAsync(
            chatEntity => chatEntity.Id == chatId,
            chatEntity => new
            {
                chatEntity.AdminsIds
            },
            cancellationToken);

        return chat != null && chat.AdminsIds.Contains(userId);
    }
}
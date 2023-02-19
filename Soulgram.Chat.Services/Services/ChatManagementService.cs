﻿using FluentValidation;
using LanguageExt.Common;
using Soulgram.Chat.Contracts.Requests;
using Soulgram.Chat.Domain.Entities;
using Soulgram.Chat.Persistence.Ports;
using Soulgram.Chat.Services.Converters;
using Soulgram.Chat.Services.Interfaces;
using Soulgram.Chat.Services.Validation.Extensions;

namespace Soulgram.Chat.Services.Services;

public class ChatManagementService : IChatManagementService
{
    private readonly IValidator<CreateChatRequest> _createValidator;
    private readonly IValidator<DeleteChatRequest> _deleteValidator;
    private readonly IRepository<ChatEntity> _repository;

    public ChatManagementService(
        IRepository<ChatEntity> repository,
        IValidator<CreateChatRequest> createValidator,
        IValidator<DeleteChatRequest> deleteValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _deleteValidator = deleteValidator;
    }

    public async Task<Result<bool>> CreateChatAsync(CreateChatRequest request, CancellationToken cancellationToken)
    {
        var result = await _createValidator.ValidateWithResultAsync(request, cancellationToken);
        if (!result.IsSuccess) return result;

        var chatEntity = request.ToChatEntity();

        await _repository.InsertOneAsync(chatEntity, cancellationToken);
        return true;
    }

    public async Task<Result<bool>> DeleteChatAsync(DeleteChatRequest request, CancellationToken cancellationToken)
    {
        var result = await _deleteValidator.ValidateWithResultAsync(request, cancellationToken);
        if (!result.IsSuccess) return result;

        await _repository.DeleteOneAsync(
            x => x.Id == request.ChatId,
            cancellationToken);

        return true;
    }
}
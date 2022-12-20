using FluentValidation;
using LanguageExt.Common;
using Soulgram.Chat.Contracts.Requests;
using Soulgram.Chat.Domain.Entities;
using Soulgram.Chat.Domain.Models;
using Soulgram.Chat.Infrastructure.Ports;
using Soulgram.Chat.Services.Converters;
using Soulgram.Chat.Services.Interfaces;

namespace Soulgram.Chat.Services.Services;

public class MessageService : IMessageService
{
    private readonly IValidator<CreateMessageRequestDto> _createValidator;
    private readonly IValidator<DeleteMessageRequest> _deleteValidator;

    private readonly IChatFileManager _fileManager;
    private readonly IChatRepository _repository;


    public MessageService(
        IChatFileManager fileManager,
        IChatRepository repository,
        IValidator<CreateMessageRequestDto> createValidator,
        IValidator<DeleteMessageRequest> deleteValidator)
    {
        _fileManager = fileManager;
        _repository = repository;
        _createValidator = createValidator;
        _deleteValidator = deleteValidator;
    }

    public async Task<Result<bool>> SendMessageAsync(CreateMessageRequestDto requestDto,
        CancellationToken cancellationToken)
    {
        var validationResult = await _createValidator.ValidateAsync(requestDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            var validationException = new ValidationException(validationResult.Errors);
            return new Result<bool>(validationException);
        }

        var attachments = requestDto.Files == null || requestDto.Files.Length() == 0
            ? Array.Empty<AttachmentEntity>()
            : await UploadFilesAndGetEntitiesAsync(requestDto);

        var messageEntity = new MessageEntity
        {
            SenderId = requestDto.SenderId,
            Text = requestDto.Text,
            Attachments = attachments
        };

        await _repository.AddMessageAsync(requestDto.ChatId, messageEntity);
        return true;
    }

    public async Task<Result<bool>> DeleteMessageAsync(DeleteMessageRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _deleteValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var validationException = new ValidationException(validationResult.Errors);
            return new Result<bool>(validationException);
        }

        var message = await _repository.GetMessageAsync(
            request.ChatId,
            request.MessageId,
            cancellationToken);

        if (message?.Attachments != null && message.Attachments.Any())
            foreach (var attachment in message.Attachments)
                await _fileManager.DeleteFileAsync(attachment.ResourceLink);

        await _repository.DeleteMessageAsync(request.ChatId, request.MessageId);

        return true;
    }

    private async Task<AttachmentEntity[]> UploadFilesAndGetEntitiesAsync(CreateMessageRequestDto requestDto)
    {
        var attachmentLinks = await _fileManager
            .UploadFilesAndGetIds(requestDto.Files, requestDto.SenderId);

        return GetAttachmentEntities(attachmentLinks.ToArray(), requestDto.Files!.ToArray())
            .ToArray();
    }

    private static IEnumerable<AttachmentEntity> GetAttachmentEntities(
        string[] attachmentIdsArray,
        FileInfoDto[] filesDto)
    {
        for (var i = 0; i < attachmentIdsArray.Length; i++)
        {
            var attachmentType = filesDto[i].ContentType.ToAttachmentType();

            yield return new AttachmentEntity
            {
                ResourceLink = attachmentIdsArray[i],
                Name = filesDto[i].Name,
                AttachmentType = attachmentType
            };
        }
    }
}
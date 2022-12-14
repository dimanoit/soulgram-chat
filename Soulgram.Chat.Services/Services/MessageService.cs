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
    private readonly IChatFileManager _fileManager;
    private readonly IChatRepository _repository;
    private readonly IValidator<CreateMessageRequestDto> _validator;

    public MessageService(
        IChatFileManager fileManager, 
        IChatRepository repository,
        IValidator<CreateMessageRequestDto> validator)
    {
        _fileManager = fileManager;
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<bool>> SendMessageAsync(CreateMessageRequestDto requestDto, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(requestDto, cancellationToken);
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

        await _repository.AddMessage(requestDto.ChatId, messageEntity);
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
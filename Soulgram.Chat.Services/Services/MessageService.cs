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

    public MessageService(IChatFileManager fileManager, IChatRepository repository)
    {
        _fileManager = fileManager;
        _repository = repository;
    }

    public async Task SendMessageAsync(CreateMessageRequestDto requestDto, CancellationToken cancellationToken)
    {
        var attachments = requestDto.Files?.Any() == false
            ? Array.Empty<AttachmentEntity>()
            : await UploadFilesAndGetEntitiesAsync(requestDto);

        var messageEntity = new MessageEntity
        {
            SenderId = requestDto.SenderId,
            Text = requestDto.Text,
            Attachments = attachments
        };

        await _repository.AddMessage(requestDto.ChatId, messageEntity);
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
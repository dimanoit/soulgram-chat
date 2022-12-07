using Microsoft.AspNetCore.Mvc;
using Soulgram.Chat.Api.Models;
using Soulgram.Chat.Services.Interfaces;

namespace Soulgram.Chat.Api.Controllers;

[Route("api/message")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _service;

    public MessageController(IMessageService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task CreateMessage([FromForm] CreateMessageRequest request,
        CancellationToken cancellationToken)
    {
        var requestDto = request.ToCreateMessageRequestDto();
        await _service.SendMessageAsync(requestDto, cancellationToken);
    }
}
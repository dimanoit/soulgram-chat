using Microsoft.AspNetCore.Mvc;
using Soulgram.Chat.Contracts;
using Soulgram.Chat.Services.Interfaces;

namespace Soulgram.Chat.Api.Controllers;

[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IChatManagementService _service;

    public ChatController(IChatManagementService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task CreateChatAsync([FromBody] CreateChatRequest request, CancellationToken cancellationToken)
    {
        await _service.CreateChatAsync(request, cancellationToken);
    }
}
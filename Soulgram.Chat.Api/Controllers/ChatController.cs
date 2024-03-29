﻿using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Chat.Contracts.Requests;
using Soulgram.Chat.Services.Interfaces;

namespace Soulgram.Chat.Api.Controllers;

[Route("api/chat")]
public class ChatController : EnrichedApiController
{
    private readonly IChatManagementService _service;

    public ChatController(IChatManagementService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateChatAsync([FromBody] CreateChatRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _service.CreateChatAsync(request, cancellationToken);
        return GetHandledResult(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteChatAsync([FromBody] DeleteChatRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _service.DeleteChatAsync(request, cancellationToken);
        return GetHandledResult(result);
    }
}
using Microsoft.AspNetCore.Mvc;
using Soulgram.Chat.Contracts.Requests;
using Soulgram.Chat.Services.Interfaces;

namespace Soulgram.Chat.Api.Controllers;

[Route("api/rights-management")]
public class RightsManageController : EnrichedApiController
{
    private readonly IAccessManageService _service;

    public RightsManageController(IAccessManageService service)
    {
        _service = service;
    }

    [HttpPut]
    public async Task<IActionResult> SetGroupAdmins([FromBody] UpdateGroupAdminsRequest request)
    {
        var result = await _service.UpdateAdminsInGroup(request);
        return GetHandledResult(result);
    }
}
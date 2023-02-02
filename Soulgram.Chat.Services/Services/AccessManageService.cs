using FluentValidation;
using LanguageExt.Common;
using Soulgram.Chat.Contracts.Requests;
using Soulgram.Chat.Infrastructure.Ports;
using Soulgram.Chat.Services.Interfaces;
using Soulgram.Chat.Services.Validation.Extensions;

namespace Soulgram.Chat.Services.Services;

public class AccessManageService : IAccessManageService
{
    private readonly IChatRepository _repository;
    private readonly IValidator<UpdateGroupAdminsRequest> _validator;

    public AccessManageService(
        IValidator<UpdateGroupAdminsRequest> validator,
        IChatRepository repository)
    {
        _validator = validator;
        _repository = repository;
    }

    public async Task<Result<bool>> UpdateAdminsInGroup(UpdateGroupAdminsRequest request)
    {
        var validationResult = await _validator.ValidateWithResultAsync(request, default);
        if (!validationResult.IsSuccess) return validationResult;

        await _repository.SetAdminListAsync(request.GroupId, request.AdminsIds);
        return true;
    }
}
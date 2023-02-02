using FluentValidation;
using Soulgram.Chat.Contracts.Requests;
using Soulgram.Chat.Infrastructure.Ports;
using Soulgram.Chat.Services.Interfaces;

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

    public Task UpdateAdminsInGroup(UpdateGroupAdminsRequest request)
    {
    }
}
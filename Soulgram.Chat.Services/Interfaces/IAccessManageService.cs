using Soulgram.Chat.Contracts.Requests;

namespace Soulgram.Chat.Services.Interfaces;

public interface IAccessManageService
{
    Task UpdateAdminsInGroup(UpdateGroupAdminsRequest request);
}
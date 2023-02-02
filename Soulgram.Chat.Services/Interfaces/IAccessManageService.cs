using LanguageExt.Common;
using Soulgram.Chat.Contracts.Requests;

namespace Soulgram.Chat.Services.Interfaces;

public interface IAccessManageService
{
    Task<Result<bool>> UpdateAdminsInGroup(UpdateGroupAdminsRequest request);
}
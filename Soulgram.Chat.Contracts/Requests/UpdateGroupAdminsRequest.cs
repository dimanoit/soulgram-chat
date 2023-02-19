namespace Soulgram.Chat.Contracts.Requests;

public record UpdateGroupAdminsRequest
{
    public string UserId { get; init; } = null!;
    public string GroupId { get; set; } = null!;
    public string[] AdminsIds { get; set; } = null!;
}
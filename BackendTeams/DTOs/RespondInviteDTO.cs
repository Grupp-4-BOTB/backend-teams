namespace BackendTeams.DTOs;

public class RespondInviteDTO
{
    public string InvitationId { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public bool Accept { get; set; }
}

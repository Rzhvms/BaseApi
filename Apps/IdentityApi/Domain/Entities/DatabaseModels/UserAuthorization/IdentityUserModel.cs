namespace Domain.Entities.UserAuthorization;

public sealed class IdentityUserModel
{
    public Guid Id { get; init; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public DateTime CreateAtDate { get; set; }
    public DateTime UpdateAtDate { get; set; }
    public bool LockoutEnabled { get; set; }
    public DateTime? LockoutEndDateUtc { get; set; } 
}
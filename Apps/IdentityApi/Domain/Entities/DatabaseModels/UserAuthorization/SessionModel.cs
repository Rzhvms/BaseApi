namespace Domain.Entities.UserAuthorization;

public sealed class SessionModel
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
}
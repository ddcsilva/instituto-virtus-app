namespace InstitutoVirtusApp.Domain.Interfaces;

public interface IUserContext
{
    string? UserId { get; }
    string? Email { get; }
    string? Name { get; }
}

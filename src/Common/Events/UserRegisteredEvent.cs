namespace Common;

public record UserRegisteredEvent(string Email) : IDomainEvent {}

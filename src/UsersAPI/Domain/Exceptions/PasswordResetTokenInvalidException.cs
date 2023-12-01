using Common;

namespace Domain.Exceptions;

public class PasswordResetTokenInvalidException : SolutionBaseException
{
    public PasswordResetTokenInvalidException(ExceptionDetails exceptionDetails) : base(exceptionDetails)
    {
    }
}

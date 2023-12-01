using Common;

namespace Domain.Exceptions;

public class EmailConfirmationFailedException : SolutionBaseException
{
    public EmailConfirmationFailedException(ExceptionDetails exceptionDetails) : base(exceptionDetails)
    {
    }
}

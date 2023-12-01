using Common;

namespace Domain.Exceptions;

public class EmailAlreadyTakenException : SolutionBaseException
{
    public EmailAlreadyTakenException(ExceptionDetails exceptionDetails) : base(exceptionDetails)
    {
    }
}

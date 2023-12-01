using Common;

namespace Domain.Exceptions;

public class EmailAlreadyConfirmedException : SolutionBaseException
{
    public EmailAlreadyConfirmedException(ExceptionDetails exceptionDetails) : base(exceptionDetails)
    {
    }
}

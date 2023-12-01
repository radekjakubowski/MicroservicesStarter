using Common;

namespace Domain.Exceptions;

public class PasswordsMustMatchException : SolutionBaseException
{
    public PasswordsMustMatchException(ExceptionDetails exceptionDetails) : base(exceptionDetails)
    {
    }
}

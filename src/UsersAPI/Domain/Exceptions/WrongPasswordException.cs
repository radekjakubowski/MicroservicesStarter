using Common;

namespace Domain.Exceptions;

public class WrongPasswordException : SolutionBaseException
{
    public WrongPasswordException(ExceptionDetails exceptionDetails) : base(exceptionDetails)
    {
    }
}

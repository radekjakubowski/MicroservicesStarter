using Common;

namespace Domain.Exceptions;

public class UserNotFoundException : SolutionBaseException
{
    public UserNotFoundException(ExceptionDetails exceptionDetails) : base(exceptionDetails)
    {
    }
}

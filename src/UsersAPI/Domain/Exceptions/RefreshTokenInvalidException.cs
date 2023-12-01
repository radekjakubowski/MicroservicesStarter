using Common;

namespace Domain.Exceptions;

public class RefreshTokenInvalidException : SolutionBaseException
{
    public RefreshTokenInvalidException(ExceptionDetails exceptionDetails) : base(exceptionDetails)
    {
    }
}

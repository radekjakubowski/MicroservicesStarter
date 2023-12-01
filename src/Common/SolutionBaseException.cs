namespace Common;

public class SolutionBaseException : Exception
{
  public int StatusCode { get; init; }
  public string Description { get; init; }

  public SolutionBaseException(ExceptionDetails exceptionDetails)
  {
    StatusCode = exceptionDetails.StatusCode;
    Description = exceptionDetails.Description;
  }
}

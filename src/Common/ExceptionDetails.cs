namespace Common;

public class ExceptionDetails
{
  public int StatusCode { get; set; }
  public string Description { get; set; }

  private ExceptionDetails(int statusCode, string description)
  {
    StatusCode = statusCode; Description = description;
  }

  public static ExceptionDetails Create(int statusCode, string description)
  {
    return new ExceptionDetails(statusCode, description);
  }
}

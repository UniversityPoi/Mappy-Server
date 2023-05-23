namespace Mappy.Models.Responses
{
  public class ApiResponse
  {
    public bool IsSuccessful { get; set; } = false;

    public string Message { get; set; } = "No Message!";

    public object? Data { get; set; }
  }
}
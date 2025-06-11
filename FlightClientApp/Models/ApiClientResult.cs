namespace FlightClientApp.Models
{
    public class ApiClientResult<T>
    {
        public T? Data { get; private set; }
        public string? ErrorMessage { get; private set; }
        public bool IsSuccess => ErrorMessage == null;
        public static ApiClientResult<T> Success(T data) => new() { Data = data };
        public static ApiClientResult<T> Failure(string errorMessage) => new()
        {
            ErrorMessage = errorMessage
        };
    }
}

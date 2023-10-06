namespace adaPrueba_b.Models
{
    public interface IServiceResponse
    {
        bool Success { get; set; }
        string? Message { get; set; }
    }

    public class ServiceResponse<T> : IServiceResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
    }
}
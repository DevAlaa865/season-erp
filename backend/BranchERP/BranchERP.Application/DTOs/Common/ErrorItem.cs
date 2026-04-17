namespace BranchERP.Application.DTOs.Common
{
    public class ErrorItem
    {
        public string Code { get; set; } = string.Empty;   // ValidationError, ServerError, ...
        public string Message { get; set; } = string.Empty;
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<ErrorItem> Errors { get; set; } = new();

        public static ApiResponse<T> Ok(T data, string message = "")
            => new ApiResponse<T> { Success = true, Data = data, Message = message };

        public static ApiResponse<T> Fail(string message, List<ErrorItem>? errors = null)
            => new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<ErrorItem>()
            };
    }
}

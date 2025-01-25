namespace RentSaaS.API.ApiResponse
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }



        public ApiResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public ApiResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        // Optionally, a constructor for simpler cases (only message):
        public ApiResponse(string message)
        {
            Success = true;
            Message = message;
            Data = default(T);
        }
    }
}

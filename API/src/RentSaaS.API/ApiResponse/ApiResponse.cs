namespace RentSaaS.API.APIResponse
{
    public class APIResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }



        public APIResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public APIResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        // Optionally, a constructor for simpler cases (only message):
        public APIResponse(string message)
        {
            Success = true;
            Message = message;
            Data = default(T);
        }
    }
}

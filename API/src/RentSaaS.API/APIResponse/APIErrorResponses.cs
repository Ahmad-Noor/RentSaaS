namespace RentSaaS.API.APIResponse;

public class APIErrorResponse
{

    public string Message { get; set; }
    public int StatusCode { get; set; }



    public APIErrorResponse(int Number, string? ErrorMessage = null)
    {
        StatusCode = Number;
        Message = !string.IsNullOrEmpty(ErrorMessage) ? ErrorMessage : GetDefaultMessageForStatusCode(Number);
    }


    public string GetDefaultMessageForStatusCode(int StatusCode)
    {
        return StatusCode switch
        {
            400 => $" Bad Request ",
            401 => $" UnAuthorize",
            404 => $" Resource Not Found",
            500 => $"Error in this Path",
            _ => "No case availabe"
        };


    }

}

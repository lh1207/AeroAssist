using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AeroAssist.Models;

public class ErrorModel(string errorCode, object errorMessage) : PageModel
{
    public string ErrorCode { get; set; } = errorCode;
    public object ErrorMessage { get; set; } = errorMessage;

    public void OnGet(string errorCode)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorCode switch
        {
            "404" => "The page you are looking for does not exist.",
            "500" => "An error occurred while processing your request.",
            _ => "An error occurred while processing your request."
        };
    }
}
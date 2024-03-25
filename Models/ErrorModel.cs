using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AeroAssist.Models;

public class ErrorModel(string errorCode) : PageModel
{
    public string ErrorCode { get; set; } = errorCode;

    public void OnGet(string errorCode)
    {
        ErrorCode = errorCode;
    }
}
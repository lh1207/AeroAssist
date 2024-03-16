using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AeroAssist.Models;

public class ErrorModel : PageModel
{
    public string ErrorCode { get; set; }

    public void OnGet(string errorCode)
    {
        ErrorCode = errorCode;
    }
}
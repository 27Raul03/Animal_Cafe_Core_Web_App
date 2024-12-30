using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Animal_Cafe_Core_Web_App.Pages
{
    public class ErrorModel : PageModel
    {
        [BindProperty]
        public string ErrorMessage { get; set; } = "An unknown error occurred.";

        public void OnGet(string? errorMessage = null)
        {
            ErrorMessage = errorMessage ?? "An unknown error occurred.";
        }
    }

}

using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Animal_Cafe_Core_Web_App.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Animal_Cafe_Core_Web_App.Data;
using Microsoft.AspNetCore.Authorization;

namespace Animal_Cafe_Core_Web_App.Pages.Animals
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly Animal_Cafe_Core_Web_AppContext _context;

        public CreateModel(Animal_Cafe_Core_Web_AppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Animal Animal { get; set; }

        [BindProperty]
        public IFormFile AnimalPhoto { get; set; } 

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Salvează imaginea ca array de bytes
            if (AnimalPhoto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await AnimalPhoto.CopyToAsync(memoryStream);
                    Animal.AnimalPhoto = memoryStream.ToArray();
                }
            }

            _context.Animal.Add(Animal);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

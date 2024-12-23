using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Animal_Cafe_Core_Web_App.Data;
using Animal_Cafe_Core_Web_App.Models;

namespace Animal_Cafe_Core_Web_App.Pages.Animals
{
    public class DeleteModel : PageModel
    {
        private readonly Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext _context;

        public DeleteModel(Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Animal Animal { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FirstOrDefaultAsync(m => m.ID == id);

            if (animal == null)
            {
                return NotFound();
            }
            else
            {
                Animal = animal;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);
            if (animal != null)
            {
                Animal = animal;
                _context.Animal.Remove(Animal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

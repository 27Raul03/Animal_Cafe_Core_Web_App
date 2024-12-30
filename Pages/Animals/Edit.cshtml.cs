using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animal_Cafe_Core_Web_App.Data;
using Animal_Cafe_Core_Web_App.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Animal_Cafe_Core_Web_App.Pages.Animals
{
    [Authorize(Roles = "Admin")]

    public class EditModel : PageModel
    {
        private readonly Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext _context;

        public EditModel(Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Animal Animal { get; set; } = default!;

        [BindProperty]
        public IFormFile? AnimalPhoto { get; set; } // Pentru încărcarea imaginii

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
            Animal = animal;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var animalToUpdate = await _context.Animal.FirstOrDefaultAsync(m => m.ID == Animal.ID);

            if (animalToUpdate == null)
            {
                return NotFound();
            }

            // Actualizează doar proprietățile relevante
            animalToUpdate.Name = Animal.Name;
            animalToUpdate.Breed = Animal.Breed;
            animalToUpdate.Age = Animal.Age;

            // Salvează imaginea doar dacă există un fișier încărcat
            if (AnimalPhoto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await AnimalPhoto.CopyToAsync(memoryStream);
                    animalToUpdate.AnimalPhoto = memoryStream.ToArray();
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(Animal.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.ID == id);
        }
    }
}

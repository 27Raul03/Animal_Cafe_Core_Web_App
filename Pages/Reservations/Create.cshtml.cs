using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Animal_Cafe_Core_Web_App.Data;
using Animal_Cafe_Core_Web_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Animal_Cafe_Core_Web_App.Pages.Reservations
{
    [Authorize]

    public class CreateModel : PageModel
    {
        private readonly Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext _context;

        public CreateModel(Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClientID"] = new SelectList(_context.Client, "ID", "Email");
        ViewData["AnimalID"] = new SelectList(_context.Animal, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var loggedInUserEmail = User.Identity?.Name;

            var selectedClientEmail = await _context.Client
                .Where(c => c.ID == Reservation.ClientID)
                .Select(c => c.Email)
                .FirstOrDefaultAsync();

            if (!string.Equals(loggedInUserEmail, selectedClientEmail, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Reservation.ClientID", "Selected client email does not match your logged-in email.");

                ViewData["AnimalID"] = new SelectList(_context.Animal, "ID", "Name");
                ViewData["ClientID"] = new SelectList(_context.Client, "ID", "Email");

                return Page();
            }

            _context.Reservation.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

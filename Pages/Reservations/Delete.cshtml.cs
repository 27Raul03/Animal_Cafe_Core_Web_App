using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Animal_Cafe_Core_Web_App.Data;
using Animal_Cafe_Core_Web_App.Models;
using Microsoft.AspNetCore.Identity;

namespace Animal_Cafe_Core_Web_App.Pages.Reservations
{
    public class DeleteModel : PageModel
    {
        private readonly Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public DeleteModel(Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.Include(r => r.Client).Include(r => r.YourBuddy).FirstOrDefaultAsync(m => m.ID == id);

            if (reservation == null)
            {
                return NotFound();
            }
            else
            {
                Reservation = reservation;
            }

            var currentClient = await _userManager.GetUserAsync(User);
            UserManager<IdentityUser> userManager;
            if (currentClient == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "Please login!" });
            }

            if (reservation.Client.Email != currentClient.Email)
            {
                return RedirectToPage("/Error", new { errorMessage = "Only the author of the review can delete it" });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                Reservation = reservation;
                _context.Reservation.Remove(Reservation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

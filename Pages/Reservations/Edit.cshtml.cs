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
using Microsoft.AspNetCore.Identity;

namespace Animal_Cafe_Core_Web_App.Pages.Reservations
{
    public class EditModel : PageModel
    {
        private readonly Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext context, UserManager<IdentityUser> userManager)
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

            var reservation =  await _context.Reservation.Include(r => r.Client).FirstOrDefaultAsync(m => m.ID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            var currentClient = await _userManager.GetUserAsync(User);
            UserManager<IdentityUser> userManager;
            if (currentClient == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "Please login!" });
            }

            if (reservation.Client.Email != currentClient.Email)
            {
                return RedirectToPage("/Error", new { errorMessage = "Only the author of the reservation can edit it" });
            }

            Reservation = reservation;
           ViewData["ClientID"] = new SelectList(_context.Client, "ID", "Name");
           ViewData["AnimalID"] = new SelectList(_context.Animal, "ID", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            _context.Attach(Reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(Reservation.ID))
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

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.ID == id);
        }
    }
}

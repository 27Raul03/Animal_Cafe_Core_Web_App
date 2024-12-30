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

namespace Animal_Cafe_Core_Web_App.Pages.Reviews
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
        public Review Review { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review =  await _context.Review.Include(r=>r.Client).FirstOrDefaultAsync(m => m.ID == id);

            if (review == null)
            {
                return NotFound();
            }

            var currentClient = await _userManager.GetUserAsync(User);
            UserManager<IdentityUser> userManager;
                if (currentClient == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "Please login!" });
            }

            if (review.Client.Email != currentClient.Email)
            {
                return RedirectToPage("/Error", new { errorMessage = "Only the author of the review can edit it" });
            }

            Review = review;
           ViewData["AnimalID"] = new SelectList(_context.Animal, "ID", "ID");
           ViewData["ClientID"] = new SelectList(_context.Client, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            _context.Attach(Review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(Review.ID))
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

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.ID == id);
        }
    }
}

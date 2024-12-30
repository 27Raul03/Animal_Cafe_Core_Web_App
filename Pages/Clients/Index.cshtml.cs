using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Animal_Cafe_Core_Web_App.Data;
using Animal_Cafe_Core_Web_App.Models;
using Microsoft.AspNetCore.Authorization;

namespace Animal_Cafe_Core_Web_App.Pages.Clients
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly Animal_Cafe_Core_Web_AppContext _context;

        public IndexModel(Animal_Cafe_Core_Web_AppContext context)
        {
            _context = context;
        }

        // Lista utilizatorilor din baza de date
        public IList<Client> ClientList { get; set; } = new List<Client>();

        // Email-ul utilizatorului conectat
        public string CurrentUserEmail { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            // Obține email-ul utilizatorului conectat
            CurrentUserEmail = User?.Identity?.Name ?? string.Empty;

            // Obține lista utilizatorilor din baza de date
            ClientList = await _context.Client.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Obține email-ul utilizatorului conectat
            var currentUserEmail = User?.Identity?.Name ?? string.Empty;

            // Găsește utilizatorul care trebuie șters
            var userToDelete = await _context.Client.FindAsync(id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            // Șterge utilizatorul
            _context.Client.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
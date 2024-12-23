using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Animal_Cafe_Core_Web_App.Data;
using Animal_Cafe_Core_Web_App.Models;

namespace Animal_Cafe_Core_Web_App.Pages.Clients
{
    public class DeleteModel : PageModel
    {
        private readonly Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext _context;

        public DeleteModel(Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Client Client { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FirstOrDefaultAsync(m => m.ID == id);

            if (client == null)
            {
                return NotFound();
            }
            else
            {
                Client = client;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client != null)
            {
                Client = client;
                _context.Client.Remove(Client);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

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
    public class IndexModel : PageModel
    {
        private readonly Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext _context;

        public IndexModel(Animal_Cafe_Core_Web_App.Data.Animal_Cafe_Core_Web_AppContext context)
        {
            _context = context;
        }

        public IList<Client> Client { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Client = await _context.Client.ToListAsync();
        }
    }
}

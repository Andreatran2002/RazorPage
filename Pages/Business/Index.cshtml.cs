using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace entity_fr.Pages_Business
{
    public class IndexModel : PageModel
    {
        public readonly App.Models.AppDbContext _context;

        public IndexModel(App.Models.AppDbContext context)
        {
            _context = context;
        }

        public IList<Business> Business { get;set; }

        public async Task OnGetAsync()
        {
            Business = await _context.Business
                .Include(b => b.User).ToListAsync();
        }
    }
}

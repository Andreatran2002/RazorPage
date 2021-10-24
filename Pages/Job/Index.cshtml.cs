using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace entity_fr.Pages_Job
{
    public class IndexModel : PageModel
    {
        private readonly App.Models.AppDbContext _context;

        public IndexModel(App.Models.AppDbContext context)
        {
            _context = context;
        }

        public IList<Job> Job { get;set; }

        public async Task OnGetAsync()
        {
            Job = await _context.Jobs.ToListAsync();
        }
    }
}

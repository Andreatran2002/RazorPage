using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Pages_WorkStatus
{
    public class IndexModel : PageModel
    {
        private readonly App.Models.AppDbContext _context;

        public IndexModel(App.Models.AppDbContext context)
        {
            _context = context;
        }

        public IList<WorkStatus> WorkStatus { get;set; }

        public async Task OnGetAsync()
        {
            WorkStatus = await _context.WorkStatus.ToListAsync();
        }
    }
}

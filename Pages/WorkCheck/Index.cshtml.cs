using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using App.Models;
using Microsoft.AspNetCore.Identity;

namespace App.Pages_WorkCheck
{
    public class IndexModel : PageModel
    {
        private readonly App.Models.AppDbContext _context;
        public readonly UserManager<AppUser> _userManager;

        public IndexModel(App.Models.AppDbContext context,
        UserManager<AppUser> userManager )
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<WorkCheck> WorkCheck { get;set; }

        public async Task OnGetAsync()
        {
            WorkCheck = await _context.WorkChecks
                .Include(w => w.User).Include(w => w.WorkStatus).ToListAsync();
        }
    }
}

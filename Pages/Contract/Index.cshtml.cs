using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using App.Models;
using Microsoft.AspNetCore.Identity;

namespace App.Pages_Contract
{
    public class IndexModel : PageModel
    {
        public readonly App.Models.AppDbContext _context;
        public readonly UserManager<AppUser> _userManager;
        public IndexModel(App.Models.AppDbContext context, 
        UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Contract> Contract { get;set; }

        public async Task OnGetAsync()
        {
            Contract = await _context.Contracts
                .Include(c => c.employee).ToListAsync();
           
        }
    }
}

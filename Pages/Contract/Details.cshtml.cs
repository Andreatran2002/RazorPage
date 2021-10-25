using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Pages_Contract
{
    public class DetailsModel : PageModel
    {
        private readonly App.Models.AppDbContext _context;

        public DetailsModel(App.Models.AppDbContext context)
        {
            _context = context;
        }

        public Contract Contract { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contract = await _context.Contracts
                .Include(c => c.employee).FirstOrDefaultAsync(m => m.ContractId == id);

            if (Contract == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

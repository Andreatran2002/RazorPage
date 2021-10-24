using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace entity_fr.Pages_Salary
{
    public class DetailsModel : PageModel
    {
        private readonly App.Models.AppDbContext _context;

        public DetailsModel(App.Models.AppDbContext context)
        {
            _context = context;
        }

        public Salary Salary { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Salary = await _context.Salaries
                .Include(s => s.User).FirstOrDefaultAsync(m => m.SalaryId == id);

            if (Salary == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

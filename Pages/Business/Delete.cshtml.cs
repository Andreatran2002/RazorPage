using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Pages_Business
{
    public class DeleteModel : PageModel
    {
        private readonly App.Models.AppDbContext _context;

        public DeleteModel(App.Models.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Business Business { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Business = await _context.Business
                .Include(b => b.User).Include(j=>j.JobName).FirstOrDefaultAsync(m => m.BusinessId == id);

            if (Business == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Business = await _context.Business.FindAsync(id);

            if (Business != null)
            {
                _context.Business.Remove(Business);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace entity_fr.Pages_Business
{
    public class EditModel : PageModel
    {
        private readonly App.Models.AppDbContext _context;

        public EditModel(App.Models.AppDbContext context)
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
                .Include(b => b.User).FirstOrDefaultAsync(m => m.BusinessId == id);

            if (Business == null)
            {
                return NotFound();
            }
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
           ViewData["JobName"] = new SelectList(_context.Jobs, "JobId", "JobName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Business).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessExists(Business.BusinessId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BusinessExists(string id)
        {
            return _context.Business.Any(e => e.BusinessId == id);
        }
    }
}

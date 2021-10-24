using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace entity_fr.Pages_WorkCheck
{
    public class EditModel : PageModel
    {
        private readonly App.Models.AppDbContext _context;

        public EditModel(App.Models.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WorkCheck WorkCheck { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WorkCheck = await _context.WorkChecks
                .Include(w => w.User).FirstOrDefaultAsync(m => m.WorkCheckId == id);

            if (WorkCheck == null)
            {
                return NotFound();
            }
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
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

            _context.Attach(WorkCheck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkCheckExists(WorkCheck.WorkCheckId))
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

        private bool WorkCheckExists(string id)
        {
            return _context.WorkChecks.Any(e => e.WorkCheckId == id);
        }
    }
}

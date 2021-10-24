using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Models;

namespace entity_fr.Pages_Business
{
    public class CreateModel : PageModel
    {
        private readonly App.Models.AppDbContext _context;

        public CreateModel(App.Models.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["JobName"] = new SelectList(_context.Jobs, "JobId", "JobName");
            return Page();
        }

        [BindProperty]
        public Business Business { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Business.BusinessId = Guid.NewGuid().ToString(); 

            _context.Business.Add(Business);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
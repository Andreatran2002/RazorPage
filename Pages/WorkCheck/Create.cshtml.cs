using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Models;

namespace App.Pages_WorkCheck
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
        ViewData["WorkStatus"] = new SelectList(_context.WorkStatus, "WorkStatusId", "Status");
            return Page();
        }

        [BindProperty]
        public WorkCheck WorkCheck { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            WorkCheck.WorkCheckId = Guid.NewGuid().ToString() ;
            _context.WorkChecks.Add(WorkCheck);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

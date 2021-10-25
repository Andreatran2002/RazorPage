using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Models;

namespace App.Pages_WorkStatus
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
            return Page();
        }

        [BindProperty]
        public WorkStatus WorkStatus { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            WorkStatus.WorkStatusId = Guid.NewGuid().ToString() ;
            _context.WorkStatus.Add(WorkStatus);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

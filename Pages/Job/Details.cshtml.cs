using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace entity_fr.Pages_Job
{
    public class DetailsModel : PageModel
    {
        private readonly App.Models.AppDbContext _context;

        public DetailsModel(App.Models.AppDbContext context)
        {
            _context = context;
        }

        public Job Job { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Job = await _context.Jobs.FirstOrDefaultAsync(m => m.JobId == id);

            if (Job == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

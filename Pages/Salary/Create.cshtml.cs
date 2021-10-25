using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Models;
using Microsoft.AspNetCore.Identity;

namespace App.Pages_Salary
{
    public class CreateModel : PageModel
    {
        private readonly App.Models.AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CreateModel(App.Models.AppDbContext context,
         UserManager<AppUser> userManager )
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return Page();
        }

        [BindProperty]
        public Salary Salary { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var hasUser = (from s in _context.Salaries
                            where s.UserId == Salary.UserId
                            select s).FirstOrDefault(); 
            if (hasUser == null){

            Salary.SalaryId =  Guid.NewGuid().ToString() ; 
            _context.Salaries.Add(Salary);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
            }
            else return Content("Đã có thông số lương của nhân viên ");
        }
    }
}

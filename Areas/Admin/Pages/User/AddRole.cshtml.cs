using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Admin.User
{
    public class AddRoleModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager; 
        private readonly AppDbContext _context; 
        public AddRoleModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager, 
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context; 
        }
        [TempData]

        public string StatusMessage { get; set; }

        public AppUser user{set;get;}
        public List<IdentityRoleClaim<string>> claimInRole{set;get; }
        public List<IdentityUserClaim<string>> claimInUserClaim{set;get; }

        [BindProperty]
        [Display(Name = "Các role gán cho user")]
        public string[] RoleNames { get;set;}
        public SelectList allRoles {set;get;}
 
        public async Task<IActionResult> OnGetAsync(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return NotFound($"Không có user");
            }

            user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Không thấy user với Id :  '{id}'.");
            }

            RoleNames = (await _userManager.GetRolesAsync(user)).ToArray<string>(); 
            
            List<string> roleNames = await _roleManager.Roles.Select(r =>r.Name).ToListAsync();
            allRoles = new SelectList(roleNames);
            await GetClaims(id);
            
            return Page();
        }

        async Task GetClaims(string id){
            // Lấy ra các role của user 
            var listRoles = from r in _context.Roles
                            join ur in _context.UserRoles on r.Id equals ur.RoleId
                            where ur.UserId == id
                            select r; 

            var _claimInRole = from c in _context.RoleClaims
                               join r in listRoles on c.RoleId equals r.Id
                               select c;
            claimInRole = await _claimInRole.ToListAsync(); 
            claimInUserClaim = await (from c in _context.UserClaims 
                                where c.UserId == id select c).ToListAsync(); 
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound($"Không có user");
            }

             user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Không thấy user với Id :  '{id}'.");
            }

            //RoleNames
            var oldRoleNames = (await _userManager.GetRolesAsync(user)).ToArray();

            var deleteRoles = oldRoleNames.Where(r => !RoleNames.Contains(r)); 
            var addRoles = RoleNames.Where(r => !oldRoleNames.Contains(r)); 
            List<string> roleNames = await _roleManager.Roles.Select(r =>r.Name).ToListAsync();
            allRoles = new SelectList(roleNames);
            // var resultAdd = await _userManager.RemoveFromRoleAsync(user, addRoles); 
            foreach (var role in deleteRoles){
                var resultDelete = await _userManager.RemoveFromRoleAsync(user,role);
                if (!resultDelete.Succeeded) {
                    resultDelete.Errors.ToList().ForEach(error =>
                    {
                    ModelState.AddModelError(string.Empty,error.Description); 
                    }); 
                    return Page();
                }
            }
            foreach (var role in addRoles){
                var resultDelete = await _userManager.AddToRoleAsync(user,role);
                if (!resultDelete.Succeeded) {
                    resultDelete.Errors.ToList().ForEach(error =>
                    {
                    ModelState.AddModelError(string.Empty,error.Description); 
                    }); 
                    return Page();
                }
            }
            StatusMessage = $"Vừa cập nhập role cho user : {user.UserName} ";

             return RedirectToPage("./Index");
        }
    }
}

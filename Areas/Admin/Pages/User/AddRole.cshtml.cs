using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using entity_fr.models;
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
        public AddRoleModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [TempData]

        public string StatusMessage { get; set; }

        public AppUser user{set;get;}

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
            return Page();
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

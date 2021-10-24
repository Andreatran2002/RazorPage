using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Admin.Role
{
    public class CreateModel : RolePageModel
    {
        public CreateModel(RoleManager<IdentityRole> roleManager, AppDbContext myBlogContext) : base(roleManager, myBlogContext)
        {
        }

        public class InputModel
        {
            [Display(Name="Tên của role")]
            [Required(ErrorMessage="Phải nhập tên của {0}")]
            [StringLength(256,MinimumLength=3,ErrorMessage="Phải nhập từ {2} đến {1} kí tự ")]
            public string Name{set;get;}
        }

        [BindProperty]
        public InputModel Input{set;get;}

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync (){

            if (!ModelState.IsValid){
                return Page(); 
            }
            var newRole = new IdentityRole(Input.Name); 
            var result = await _roleManager.CreateAsync(newRole);   

            if(result.Succeeded)
            {
                StatusMessage = "Bạn vừa tạo role mới : "+ Input.Name; 
                return RedirectToPage("./Index"); 
            } 
            else
            {
                result.Errors.ToList().ForEach(error =>
                {
                     ModelState.AddModelError(string.Empty,error.Description); 
                });

            }

            return Page(); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using entity_fr.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Admin.Role
{
    public class AddRoleClaimModel : RolePageModel
    {
        public AddRoleClaimModel(RoleManager<IdentityRole> roleManager, MyBlogContext myBlogContext) : base(roleManager, myBlogContext)
        {
        }

        public class InputModel
        {
            [Display(Name="Kiểu (tên) claim")]
            [Required(ErrorMessage="Phải nhập tên của {0}")]
            [StringLength(256,MinimumLength=3,ErrorMessage="Phải nhập từ {2} đến {1} kí tự ")]
            public string ClaimType{set;get;}
            [Display(Name="Giá trị")]
            [Required(ErrorMessage="Phải nhập tên của {0}")]
            [StringLength(256,MinimumLength=3,ErrorMessage="Phải nhập từ {2} đến {1} kí tự ")]
            public string ClaimValue{set;get;}
        }

        [BindProperty]
        public InputModel Input{set;get;}
        public IdentityRole role{set;get;}
        public async Task<IActionResult> OnGet(string roleid)
        {
            role = await _roleManager.FindByIdAsync(roleid);
            if (role==null) return Content("Không tìm thấy role"); 
            return Page();
        }
        public async Task<IActionResult> OnPostAsync (string roleid){
            role = await _roleManager.FindByIdAsync(roleid);
            if (role==null) return Content("Không tìm thấy role"); 
            if (!ModelState.IsValid){
                return Page(); 
            }

            if((await _roleManager.GetClaimsAsync(role))
                .Any(c => c.Type == Input.ClaimType && c.Value == Input.ClaimValue))
            {
                ModelState.AddModelError(string.Empty,"Claim này đã có");
                return Page();
            } 


            var newClaim = new Claim(Input.ClaimType, Input.ClaimValue);  
            var result = await _roleManager.AddClaimAsync(role,newClaim);

            if(result.Succeeded)
            {
                StatusMessage = "Bạn vừa tạo một claim mới : "; 
                return RedirectToPage("./Edit",new{roleid = role.Id}); 
            } 
            else
            {
                result.Errors.ToList().ForEach(error =>
                {
                     ModelState.AddModelError(string.Empty,error.Description); 
                });

            }

            return RedirectToPage("./Edit",new{roleid = role.Id});  
        }
    }
}

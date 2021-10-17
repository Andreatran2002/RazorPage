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
using Microsoft.EntityFrameworkCore;

namespace App.Admin.User
{
    public class EditUserRoleClaimModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly MyBlogContext _context;


        public EditUserRoleClaimModel(MyBlogContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [TempData]
        public string StatusMessage { set; get; }

        public class InputModel
        {
            [Display(Name = "Kiểu (tên) claim")]
            [Required(ErrorMessage = "Phải nhập tên của {0}")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "Phải nhập từ {2} đến {1} kí tự ")]
            public string ClaimType { set; get; }
            [Display(Name = "Giá trị")]
            [Required(ErrorMessage = "Phải nhập tên của {0}")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "Phải nhập từ {2} đến {1} kí tự ")]
            public string ClaimValue { set; get; }
        }

        [BindProperty]
        public InputModel Input { set; get; }
        public IdentityRoleClaim<string> claim { set; get; }

        public AppUser user { set; get; }
        public async Task<IActionResult> OnGetAddClaimAsync(string id)
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
            return Page();

        }
        public async Task<IActionResult> OnPostAddClaimAsync(string id)
        {
            user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound($"Không thấy user với Id :  '{id}'.");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newClaim = new Claim(Input.ClaimType, Input.ClaimValue);
            var claims = _context.UserClaims.Where(c => c.UserId == user.Id);
            if (claims.Any(c => c.ClaimType == Input.ClaimType && c.ClaimValue == Input.ClaimValue))
            {
                ModelState.AddModelError(string.Empty, "Claim đã tồn tại");
                return Page();
            }
            var result = await _userManager.AddClaimAsync(user, new Claim(Input.ClaimType, Input.ClaimValue));

            if (result.Succeeded)
            {
                StatusMessage = "Bạn vừa tạo một claim mới  ";
                return RedirectToPage("./AddRole", new { id = id });
            }
            else
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });

            }

            return RedirectToPage("./AddRole", new { id = id });
        }
        public IdentityUserClaim<string> userClaim { set; get; }
        public async Task<IActionResult> OnGetEditClaimAsync(int? claimid)
        {
            if (claimid == null)
            {
                return NotFound($"Không có claim");
            }
            userClaim = _context.UserClaims.Where(c => c.Id == claimid).FirstOrDefault();
            user = await _userManager.FindByIdAsync(userClaim.UserId);
            if (user == null) return NotFound("Không tìm thấy user");

            Input = new InputModel()
            {
                ClaimType = userClaim.ClaimType,
                ClaimValue = userClaim.ClaimValue
            };

            // await _context.SaveChangesAsync(); 

            return Page();
        }
        public async Task<IActionResult> OnPostEditClaimAsync(int? claimid)
        {

            if (claimid == null)
            {
                return NotFound($"Không có claim");
            }
            userClaim = _context.UserClaims.Where(c => c.Id == claimid).FirstOrDefault();
            user = await _userManager.FindByIdAsync(userClaim.UserId);
            if (user == null) return NotFound("Không tìm thấy user");
            if (!ModelState.IsValid) return Page();
            if(_context.UserClaims.Any( c => c.UserId == user.Id && c.ClaimType == Input.ClaimType && c.ClaimValue == Input.ClaimValue && c.Id != userClaim.Id)){
                ModelState.AddModelError(string.Empty,"Claim này đã có");
                return Page(); 
            }

            userClaim.ClaimType = Input.ClaimType;
            userClaim.ClaimValue = Input.ClaimValue;

            await _context.SaveChangesAsync(); 
            StatusMessage = "Bạn vừa cập nhập claim thành công "; 
            return RedirectToPage("./AddRole", new { id = user.Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? claimid)
        {

            if (claimid == null)
            {
                return NotFound($"Không có claim");
            }
            userClaim = _context.UserClaims.Where(c => c.Id == claimid).FirstOrDefault();
            user = await _userManager.FindByIdAsync(userClaim.UserId);
            if (user == null) return NotFound("Không tìm thấy user");
            
            await _userManager.RemoveClaimAsync(user,new Claim(userClaim.ClaimType,userClaim.ClaimValue)); 

            StatusMessage = "Bạn vừa xóa claim thành công "; 
            return RedirectToPage("./AddRole", new { id = user.Id });
        }
    }
}

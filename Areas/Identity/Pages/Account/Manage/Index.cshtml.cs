using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

    namespace App.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private IHostingEnvironment _environment;
        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, 
            IHostingEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
        }

        public string Username { get; set; }
        public IFormFile Avatar{set;get;}

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }


        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Địa chỉ thường trú")]
            public string HomeAddress { get; set; }

            [Display(Name = "Quê quán")]
            public string Hometown { get; set; }

            [Display(Name = "Giói tính")]
            public string Sex { get; set; }

            [Display(Name = "Tôn giáo")]
            public string Religion { get; set; }

            [Display(Name = "Ngày sinh")]
            public DateTime? BirthDate { get; set; }
            
        }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                HomeAddress = user.HomeAddress,
                BirthDate = user.BirthDate,
                Hometown = user.Hometown,
                Sex= user.Sex,
                Religion = user.Religion,



            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (user.Avatar != null)avatar_file = "/uploads/" + user.Id+".jpg"; 
            await LoadAsync(user);
            return Page();
        }
        public AppUser user{set;get;}
        public string avatar_file { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            // if (Input.PhoneNumber != phoneNumber)
            // {
            //     var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //     if (!setPhoneResult.Succeeded)
            //     {
            //         StatusMessage = "Unexpected error when trying to set phone number.";
            //         return RedirectToPage();
            //     }
            // }
            user.HomeAddress = Input.HomeAddress;
            user.PhoneNumber = Input.PhoneNumber; 
            user.BirthDate = Input.BirthDate; 
            user.Hometown = Input.Hometown;
            user.Sex = Input.Sex; 
            user.Religion = Input.Religion; 


            if (Avatar != null) {
                var file = Path.Combine (_environment.ContentRootPath, "wwwroot/uploads", user.Id+".jpg");
                using (var fileStream = new FileStream (file, FileMode.Create)) {
                await Avatar.CopyToAsync (fileStream);
                user.Avatar=Avatar.FileName; 
                }
            }
            await _userManager.UpdateAsync(user); 
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

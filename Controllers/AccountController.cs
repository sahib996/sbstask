using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using sbs.Models;
using sbs.ViewModels.Account;

namespace sbs.Controllers
{
    public class AccountController(UserManager<AppUser>_userManager, SignInManager<AppUser> _signInManager) : Controller
    {
        //public IActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterVM vm)
        //{
        //    if (ModelState.IsValid) return View(vm);
        //    AppUser user = new AppUser()
        //    { 
        //        Name = vm.Name,
        //        Surname = vm.Surname,
        //        Email = vm.Email,
        //        UserName = vm.UserName,

        //    };
        //     IdentityResult resul=  await _userManager.CreateAsync(user, vm.Password);
        //  if(!result.Succeeded)



        //    return View();
        //}

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginVM signIn, string ReturnUrl)
        {
            AppUser user;
            if (signIn.UserNameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(signIn.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(signIn.UserNameOrEmail);
            }
            if (user == null)
            {
                ModelState.AddModelError("", "Login ve ya parol yalnisdir");
                return View(signIn);
            }
            var result = await
            _signInManager.PasswordSignInAsync(user, signIn.Password, signIn.RememberMe, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Login ve ya parol yalnisdir");
                return View(signIn);
            }
            if (ReturnUrl != null) return LocalRedirect(ReturnUrl);
            return RedirectToAction("Index", "Team", new
            {
                area = "admin"
            });

        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            AppUser newUser = new AppUser
            {
                Email = register.Email,
                UserName = register.UserName
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
    }
}

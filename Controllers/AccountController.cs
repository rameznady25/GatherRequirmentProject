using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Gather_Requirement_Project.ViewModel;

namespace Gather_Requirement_Project.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        // GET: /<controller>/
        // /Account/register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        //Remote Validation
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsDeveloperFound(string DeveloperName)
        {
            var user = await userManager.FindByNameAsync(DeveloperName);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Developer {DeveloperName} is already in use");
            }
        }



        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.DeveloperName };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "CustomerPrograms");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.DeveloperName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "CustomerPrograms");
                    }
                }


                ModelState.AddModelError(string.Empty, "Invalid Login");

            }
            return View(model);
        }
        // logout Action
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //sign in manager

            await signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
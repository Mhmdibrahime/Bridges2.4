using Bridges2._4.Models;
using Bridges2._4.Models.Data;
using Bridges2._4.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Security.Claims;

namespace Bridges2._4.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AppDbContext context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }


        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel newuserVM)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser usermodel = new ApplicationUser();
                usermodel.UserName = newuserVM.FName;
                usermodel.LName = newuserVM.LName;
                usermodel.Email = newuserVM.EMail;
                usermodel.PhoneNumber = newuserVM.PhoneNumber;
                usermodel.PasswordHash = newuserVM.Password;

                IdentityResult result = await userManager.CreateAsync(usermodel,newuserVM.Password);


                if (result.Succeeded)
                {
                    
                    await signInManager.SignInAsync(usermodel, true);
                    IdentityResult resultt = await userManager.AddToRoleAsync(usermodel, "User");
                    User user = new User();
                    //Admin admin = new Admin();
                    user.Id= usermodel.Id;
                    user.Name = newuserVM.FName+newuserVM.LName;
                    user.Email = newuserVM.EMail;
                    user.PhoneNumber = newuserVM.PhoneNumber;
                    user.Password = newuserVM.Password;
                   context.users.Add(user);
                    context.SaveChanges();

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var erroritem in result.Errors)
                    {
                        ModelState.AddModelError("", erroritem.Description);
                    }
                    return View(newuserVM);
                }
            }
            return View(newuserVM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(userVM.Email);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, userVM.Password);
                    if (found)
                    {
                        List<Claim> claims = new List<Claim>();
                       
                        claims.Add(new Claim(ClaimTypes.Email, user.Email));
                        
                        claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));

                        await signInManager.SignInWithClaimsAsync(user,true, claims);
                        if (User.IsInRole("Admin"))
                        {
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        return RedirectToAction("Welcome", "Home");
                    }
                }
                ModelState.AddModelError("", "Wrong Email or Password or Phone Number");
            }
            return View(userVM);
        }

        
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Welcome", "Home");
        }

    }
}

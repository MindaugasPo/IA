using System;
using System.Text;
using System.Threading.Tasks;
using IA.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Types;
using Types.DTO;
using Types.Entities;

namespace IA.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public AccountController(
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return PartialView("~/Views/Account/RegisterForm.cshtml");
        }

        [HttpPost]
        [ServiceFilter(typeof(IaValidationFilter))]
        public async Task<IActionResult> Register(RegisterDto registration)
        {
            var user = new User() { UserName = registration.Username };
            var result = await _userManager.CreateAsync(user, registration.Password);
            if (result.Succeeded)
            {
                return new JsonResult(new AjaxResult() { Success = true, Message = "Registered" });
            }
            else
            {
                StringBuilder errors = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    errors.Append(Environment.NewLine + error.Description);
                }
                return new JsonResult(new AjaxResult() { Success = false, Message = errors.ToString() });
            }
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return PartialView("~/Views/Account/SignInForm.cshtml");
        }

        [HttpPost]
        [ServiceFilter(typeof(IaValidationFilter))]
        public async Task<IActionResult> SignIn(SigninDto signinDto)
        {
            var result = await _signInManager.PasswordSignInAsync(
                signinDto.Username, 
                signinDto.Password, 
                signinDto.RememberMe,
                false);

            return result.Succeeded 
                ? new JsonResult(new AjaxResult() { Success = true, Message = "Signed in" }) 
                : new JsonResult(new AjaxResult() { Success = false, Message = "Invalid login" });
        }
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return new JsonResult(new AjaxResult() { Success = true, Message = "Signed out" });
        }
    }
}
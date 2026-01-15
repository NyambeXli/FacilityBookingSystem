using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UfsConnectBook.Models.Entities;
using UfsConnectBook.Models.ViewModel;


namespace UfsConnectBook.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string regAs = "student")
        {
            ViewBag.IsModelError = true;
            ViewBag.regAs = regAs;
            return View(new LoginModel
            {
                ReturnUrl = "/"
            });
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            ViewBag.IsModelError = true;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ActionName("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user1 = new AppUser
                {
                    Email = model.EmailAddress,
                    UserName = model.EmailAddress,
                    Name = model.Name,
                    Surname = model.Surname,
                    Identity = model.Identification

                };

                AppUser user =
                await _userManager.FindByEmailAsync(model.EmailAddress);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user,
                    model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return Redirect("/Home/Index");
                    }

                }

            }
            ModelState.AddModelError("", "Invalid email or password");

            ViewBag.IsModelError = false;
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            ViewBag.IsModelError = true;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ActionName("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var existingUser = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "The email has already been taken.");
                return View(model);
            }

            if (model.Password.Length < 6)
            {
                ModelState.AddModelError("", "Password must be at least 6 characters long.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var user =
                new AppUser
                {
                    Email = model.EmailAddress,
                    UserName = model.EmailAddress,
                    Name = model.Name,
                    Surname = model.Surname,
                    Identity = model.Identification
                };
                var result = await _userManager.CreateAsync(user,
                model.Password);

                if (result.Succeeded)
                {
                    string role = "AppUser";

                    if (!await _roleManager.RoleExistsAsync(role))
                        await _roleManager.CreateAsync(new IdentityRole { Name = role });

                    await _userManager.AddToRoleAsync(user, role);
                    return RedirectToAction("Login", "Account");
                }
            }
            ModelState.AddModelError("", "Failed to register user!");
            ViewBag.IsModelError = false;
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

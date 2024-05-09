using Domain;
using Domain.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;

namespace Front_End.Controllers
{
    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public AccountController(IWebHostEnvironment env)
        {
            _env = env;

        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {

            using (var httpClient = new HttpClient())
            {
                // return Json(blog);

                StringContent content = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7250/api/Account/register", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //blog = JsonConvert.DeserializeObject<Blog>(apiResponse);
                }
            }
            return RedirectToAction("Login", "Account");

        }

        public IActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminRegister(RegisterModel registerModel)
        {

            using (var httpClient = new HttpClient())
            {
                // return Json(blog);

                StringContent content = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7250/api/Account/register", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //blog = JsonConvert.DeserializeObject<Blog>(apiResponse);
                }
            }
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(loginViewModel), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7250/api/Account/LoginUser", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(apiResponse);

                    var token = dict["token"];


                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                    // Extract claims from the JWT
                    var claims = jsonToken?.Claims;

                    if (claims != null)
                    {
                        // Create a claims identity
                        var claimsIdentity = new ClaimsIdentity(claims, "login");

                        // Create authentication properties
                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1), // Set expiration time for the cookie
                            IsPersistent = true
                        };

                        // Sign in the user
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    }

                }
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect the user to the home page or any other desired page after logout
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string Id)
        {
            AppUser reservation = new AppUser();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7250/api/Account/get?id=" + Id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        reservation = JsonConvert.DeserializeObject<AppUser>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(AppUser appUser)
        {

            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(appUser), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"https://localhost:7250/api/Account/update/{appUser}", content))

                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //blog = JsonConvert.DeserializeObject<Blog>(apiResponse);
                }
            }
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> GetUser()
        {
            // Get the ID of the currently authenticated user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<AppUser> userList = new List<AppUser>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7250/api/Account/GetUsers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userList = JsonConvert.DeserializeObject<List<AppUser>>(apiResponse);
                }
            }

            // Filter the blog list to only include blogs created by the current user


            return View(userList);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:7250/api/Account/delete/{Id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7250/api/Account/get/{userId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<AppUser>(apiResponse);

                        var model = new ChangePassword { UserId = user.Id };
                        return View(model);
                    }
                    else
                    {
                        // Handle error response from API
                        return View("Error");
                    }
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"https://localhost:7250/api/Account/change", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Home"); // Redirect to home page or any other page after successful password change
                    }
                    else
                    {
                        // Handle error response from API
                        return View("Error");
                    }
                }
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> ChangePassword()
        //{
        //    // Fetch the current user's ID
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound("User not found.");
        //    }

        //    // Create and populate the model
        //    var model = new ChangePassword { UserId = user.Id };
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePassword model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var user = await _userManager.FindByIdAsync(model.UserId);
        //    if (user == null)
        //    {
        //        return NotFound("User not found.");
        //    }

        //    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Index", "Home"); // Redirect to home page or any other page after successful password change
        //    }

        //    // If password change fails, add errors to ModelState and return view
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError(string.Empty, error.Description);
        //    }
        //    return View(model);
        //}


    }
}

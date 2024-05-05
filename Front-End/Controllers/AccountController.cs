using Domain;
using Domain.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Front_End.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel1 registerModel)
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
            return RedirectToAction("Index", "Blog");

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
            return RedirectToAction("Index", "Blog");

        }
        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect the user to the home page or any other desired page after logout
            return RedirectToAction("Index", "Home");
        }

    }
}

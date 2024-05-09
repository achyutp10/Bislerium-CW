using Domain.Entity;
using Front_End.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Front_End.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string sortOrder = "recent")
        {
            List<Blog> blogList = new List<Blog>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7250/api/Blog/GetBlogs?page={page}&pageSize={pageSize}&sortOrder={sortOrder}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        blogList = JsonConvert.DeserializeObject<List<Blog>>(apiResponse);
                    }
                    else
                    {
                        // Handle error response
                        return View("Error");
                    }
                }
            }
            // Sort the blogList based on the sortOrder
            switch (sortOrder)
            {
                case "recent":
                    blogList = blogList.OrderByDescending(b => b.CreatedDate).ToList();
                    break;
                case "random":
                    // Shuffle the list for random sorting
                    Shuffle(blogList);
                    break;
                case "popularity":
                    blogList = blogList.OrderByDescending(b => b.Popularity).ToList();
                    break;
                default:
                    break;
            }
            // Pass pagination and sorting information to the view
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.SortOrder = sortOrder;
            // Set ViewBag properties for pagination
            ViewBag.HasPreviousPage = (page > 1);
            ViewBag.HasNextPage = (blogList.Count == pageSize);

            return View(blogList);
        }
        // algorithm for shuffling the list
        private void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

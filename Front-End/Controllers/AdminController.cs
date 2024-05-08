//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace Front_End.Controllers
//{
//    //public class AdminController : Controller
//    //{
//    //    public async Task<IActionResult> Index()
//    //    {
//    //        // Call GetAllTimeBlogPostCount to fetch the all-time blog post count
//    //        var allTimeBlogPostCount = await GetAllTimeBlogPostCount();
//    //        return View(allTimeBlogPostCount);
//    //    }
//    //    [HttpGet]
//    //    public async Task<int> GetAllTimeBlogPostCount()
//    //    {
//    //        using (var httpClient = new HttpClient())
//    //        {
//    //            try
//    //            {
//    //                HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7250/api/Admin/all-time-blog-post-count");
//    //                response.EnsureSuccessStatusCode();
//    //                var count = await response.Content.ReadAsStringAsync();
//    //                // Convert count to integer if necessary
//    //                if (int.TryParse(count, out int result))
//    //                {
//    //                    return result;
//    //                }
//    //                else
//    //                {
//    //                    // Handle parsing error
//    //                    return 0;
//    //                }
//    //            }
//    //            catch (HttpRequestException)
//    //            {
//    //                // Handle HTTP request error
//    //                return 0;
//    //            }
//    //        }
//    //    }
//        public class AdminController : Controller
//    {

//        public IActionResult Index()
//        {
//            return View();
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetAllTimeBlogPostCount()
//        {
//            using (var httpClient = new HttpClient())
//            {
//                try
//                {
//                    HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7250/api/Admin/all-time-blog-post-count");
//                    response.EnsureSuccessStatusCode();
//                    var count = await response.Content.ReadAsStringAsync();

//                    // Create a new instance of ViewBag
//                    ViewBag.AllTimeBlogPostCount = count;
//                    return View("Index", ViewBag); // Pass the ViewBag to the view

//                }
//                catch (HttpRequestException)
//                {
//                    return RedirectToAction("Error", "Home");
//                }
//            }
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllTimeCommentCount()
//        {
//            using (var httpClient = new HttpClient())
//            {
//                try
//                {
//                    HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7250/api/Admin/all-time-comment-count");
//                    response.EnsureSuccessStatusCode();
//                    var count = await response.Content.ReadAsStringAsync();
//                    return View("CommentCountView", count);
//                }
//                catch (HttpRequestException)
//                {
//                    return RedirectToAction("Error", "Home");
//                }
//            }
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetAllTimeDownvoteCount()
//        {
//            using (var httpClient = new HttpClient())
//            {
//                try
//                {
//                    HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7250/api/Admin/all-time-downvotes");
//                    response.EnsureSuccessStatusCode();
//                    var count = await response.Content.ReadAsStringAsync();
//                    return View("DownvoteCountView", count);
//                }
//                catch (HttpRequestException)
//                {
//                    return RedirectToAction("Error", "Home");
//                }
//            }
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetAllTimeUpvoteCount()
//        {
//            using (var httpClient = new HttpClient())
//            {
//                try
//                {
//                    HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7250/api/Admin/all-time-upvotes");
//                    response.EnsureSuccessStatusCode();
//                    var count = await response.Content.ReadAsStringAsync();
//                    return View("UpvoteCountView", count);
//                }
//                catch (HttpRequestException)
//                {
//                    return RedirectToAction("Error", "Home");
//                }
//            }
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetDailyBlogPostCount(DateTime date)
//        {
//            using (var httpClient = new HttpClient())
//            {
//                try
//                {
//                    HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7250/api/Admin/daily-blog-posts?date={date}");
//                    response.EnsureSuccessStatusCode();
//                    var count = await response.Content.ReadAsStringAsync();
//                    return View("DailyBlogPostCountView", count);
//                }
//                catch (HttpRequestException)
//                {
//                    return RedirectToAction("Error", "Home");
//                }
//            }
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetDailyCommentCount(DateTime date)
//        {
//            using (var httpClient = new HttpClient())
//            {
//                try
//                {
//                    HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7250/api/Admin/daily-comments?date={date}");
//                    response.EnsureSuccessStatusCode();
//                    var count = await response.Content.ReadAsStringAsync();
//                    return View("DailyCommentCountView", count);
//                }
//                catch (HttpRequestException)
//                {
//                    return RedirectToAction("Error", "Home");
//                }
//            }
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetDailyDownvoteCount(DateTime date)
//        {
//            using (var httpClient = new HttpClient())
//            {
//                try
//                {
//                    HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7250/api/Admin/daily-downvotes?date={date}");
//                    response.EnsureSuccessStatusCode();
//                    var count = await response.Content.ReadAsStringAsync();
//                    return View("DailyDownvoteCountView", count);
//                }
//                catch (HttpRequestException)
//                {
//                    return RedirectToAction("Error", "Home");
//                }
//            }
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetDailyUpvoteCount(DateTime date)
//        {
//            using (var httpClient = new HttpClient())
//            {
//                try
//                {
//                    HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7250/api/Admin/daily-upvotes?date={date}");
//                    response.EnsureSuccessStatusCode();
//                    var count = await response.Content.ReadAsStringAsync();
//                    return View("DailyUpvoteCountView", count);
//                }
//                catch (HttpRequestException)
//                {
//                    return RedirectToAction("Error", "Home");
//                }
//            }
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetTop10PopularPosts()
//        {
//            using (var httpClient = new HttpClient())
//            {
//                try
//                {
//                    HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7250/api/Admin/top-10-popular-posts");
//                    response.EnsureSuccessStatusCode();
//                    var popularPosts = await response.Content.ReadAsStringAsync();
//                    return View("Top10PopularPostsView", popularPosts);
//                }
//                catch (HttpRequestException)
//                {
//                    return RedirectToAction("Error", "Home");
//                }
//            }
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetTop10PopularBloggers()
//        {
//            using (var httpClient = new HttpClient())
//            {
//                try
//                {
//                    HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7250/api/Admin/top-10-popular-bloggers");
//                    response.EnsureSuccessStatusCode();
//                    var popularBloggers = await response.Content.ReadAsStringAsync();
//                    return View("Top10PopularBloggersView", popularBloggers);
//                }
//                catch (HttpRequestException)
//                {
//                    return RedirectToAction("Error", "Home");
//                }
//            }
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace Front_End.Controllers
{
    public class AdminController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = new AdminDashboardViewModel();

            // Fetch all-time stats
            model.AllTimeBlogPostCount = await GetAllTimeBlogPostCount();
            model.AllTimeCommentCount = await GetAllTimeCommentCount();
            model.AllTimeDownvoteCount = await GetAllTimeDownvoteCount();
            model.AllTimeUpvoteCount = await GetAllTimeUpvoteCount();

            // Fetch daily stats for today
            model.DailyBlogPostCount = await GetDailyBlogPostCount(DateTime.Today);
            model.DailyCommentCount = await GetDailyCommentCount(DateTime.Today);
            model.DailyDownvoteCount = await GetDailyDownvoteCount(DateTime.Today);
            model.DailyUpvoteCount = await GetDailyUpvoteCount(DateTime.Today);

            // Fetch top 10 popular posts and bloggers
            model.Top10PopularPosts = await GetTop10PopularPosts();
            model.Top10PopularBloggers = await GetTop10PopularBloggers();

            return View(model);
        }

        [HttpGet]
        public async Task<int> GetAllTimeBlogPostCount()
        {
            return await GetCountFromApi("https://localhost:7250/api/Admin/all-time-blog-post-count");
        }

        [HttpGet]
        public async Task<int> GetAllTimeCommentCount()
        {
            return await GetCountFromApi("https://localhost:7250/api/Admin/all-time-comment-count");
        }

        [HttpGet]
        public async Task<int> GetAllTimeDownvoteCount()
        {
            return await GetCountFromApi("https://localhost:7250/api/Admin/all-time-downvotes");
        }

        [HttpGet]
        public async Task<int> GetAllTimeUpvoteCount()
        {
            return await GetCountFromApi("https://localhost:7250/api/Admin/all-time-upvotes");
        }

        [HttpGet]
        public async Task<int> GetDailyBlogPostCount(DateTime date)
        {
            return await GetCountFromApi($"https://localhost:7250/api/Admin/daily-blog-posts?date={date}");
        }

        [HttpGet]
        public async Task<int> GetDailyCommentCount(DateTime date)
        {
            return await GetCountFromApi($"https://localhost:7250/api/Admin/daily-comments?date={date}");
        }

        [HttpGet]
        public async Task<int> GetDailyDownvoteCount(DateTime date)
        {
            return await GetCountFromApi($"https://localhost:7250/api/Admin/daily-downvotes?date={date}");
        }

        [HttpGet]
        public async Task<int> GetDailyUpvoteCount(DateTime date)
        {
            return await GetCountFromApi($"https://localhost:7250/api/Admin/daily-upvotes?date={date}");
        }

        [HttpGet]
        public async Task<string[]> GetTop10PopularPosts()
        {
            return await GetArrayFromApi("https://localhost:7250/api/Admin/top-10-popular-posts");
        }

        [HttpGet]
        public async Task<string[]> GetTop10PopularBloggers()
        {
            return await GetArrayFromApi("https://localhost:7250/api/Admin/top-10-popular-bloggers");
        }

        private async Task<int> GetCountFromApi(string url)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var count = await response.Content.ReadAsStringAsync();
                    if (int.TryParse(count, out int result))
                    {
                        return result;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (HttpRequestException)
                {
                    return 0;
                }
            }
        }

        private async Task<string[]> GetArrayFromApi(string url)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var array = await response.Content.ReadAsAsync<string[]>();
                    return array;
                }
                catch (HttpRequestException)
                {
                    return new string[0];
                }
            }
        }
    }
}




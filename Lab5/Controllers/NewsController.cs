using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Lab5.Models; 

namespace Lab5.Controllers
{
    public class NewsController : Controller
    {
        private readonly HttpClient _httpClient;

        public NewsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> News()
        {
            string apiKey = "bdb3e5e691424cca851f9d6a261baa67";
            //string apiUrl = $"https://newsapi.org/v2/top-headlines?country=us&apiKey={apiKey}"; headlight news
            string apiUrl = $"https://newsapi.org/v2/everything?q=movie&language=en&apiKey={apiKey}"; //relate of movie

            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("User-Agent", "Mozilla/5.0");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = $"Error: {response.StatusCode}";
                return View("Error");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var newsData = JsonConvert.DeserializeObject<News>(jsonResponse);

            return View(newsData);
        }

        //뉴스추가
        public async Task<IActionResult> LoadMoreNews(int startIndex)
        {
            string apiKey = "bdb3e5e691424cca851f9d6a261baa67";
            string apiUrl = $"https://newsapi.org/v2/everything?q=movie&language=en&apiKey={apiKey}";

            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("User-Agent", "Mozilla/5.0");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var newsData = JsonConvert.DeserializeObject<News>(jsonResponse);

            if (newsData?.Articles == null || startIndex >= newsData.Articles.Count)
            {
                return Json(new { success = false }); // 더 이상 기사 없음
            }

            var articles = newsData.Articles.Skip(startIndex).Take(6).ToList();
            return PartialView("NewsPartial", articles);
        }
    }
}


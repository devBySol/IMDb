using Lab5.Models;
using Lab5.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lab5.Controllers
{
    public class HomeController : Controller
    {
        // movie controller와 중복사용으로 별도의 서비스로 관리(OMDb Api)
        private readonly MovieService _movieService;

        public HomeController(MovieService movieService)
        {
            _movieService = movieService;
        }
        // 대량 데이터 처리를 위해 비동기로 사용함(로딩속도 개선)
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetMoviesAsync();
            return View(movies);
        }
        // 장르별 필터링
        public async Task<IActionResult> FilterMovies(string filter)
        {
            IEnumerable<Movie> filteredMovies;

            // 비동기로 영화 데이터 가져오기
            var allMovies = await _movieService.GetMoviesAsync();

            // 필터 조건에 맞춰 영화 목록 필터링
            switch (filter)
            {
                case "action":
                    filteredMovies = allMovies.Where(m => m.Genre.Contains("Action"));
                    break;

                case "adventure":
                    filteredMovies = allMovies.Where(m => m.Genre.Contains("Adventure"));
                    break;

                case "drama":
                    filteredMovies = allMovies.Where(m => m.Genre.Contains("Drama"));
                    break;

                case "comedy":
                    filteredMovies = allMovies.Where(m => m.Genre.Contains("Comedy"));
                    break;

                case "horror":
                    filteredMovies = allMovies.Where(m => m.Genre.Contains("Horror"));
                    break;

                case "sci-fi":
                    filteredMovies = allMovies.Where(m => m.Genre.Contains("Sci-Fi"));
                    break;

                case "animation":
                    filteredMovies = allMovies.Where(m => m.Genre.Contains("Animation"));
                    break;

                case "fantasy":
                    filteredMovies = allMovies.Where(m => m.Genre.Contains("Fantasy"));
                    break;

                // Top Rated filter
                case "top-rated":
                    filteredMovies = allMovies
                        .Where(m => double.TryParse(m.imdbRating, out _))  // 평점이 있는 경우만
                        .OrderByDescending(m => double.Parse(m.imdbRating)) // 평점 높은 순 정렬
                        .Take(10);
                    break;



                default:
                    filteredMovies = allMovies;
                    break;
            }

           // 부분만 비동기로 업데이트 (속도가 빠름빠름)
            return PartialView("_MoviePartial", filteredMovies);
        }
    }
    }


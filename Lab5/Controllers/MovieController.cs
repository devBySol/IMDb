using Lab5.Models;
using Lab5.Services; 
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace Lab5.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

    
        public async Task<IActionResult> Movies()
        {
            var movies = await _movieService.GetMoviesAsync();
            return View(movies);
        }

      
        public async Task<IActionResult> MovieDetails(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var movies = await _movieService.GetMoviesAsync();
            var movie = movies.FirstOrDefault(m => m.imdbID == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

   
       

    }
}

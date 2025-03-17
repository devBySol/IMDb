using Microsoft.AspNetCore.Mvc;
using Lab5.Data.Repositories;

namespace Lab5.Controllers
{
    public class CommunityController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public CommunityController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public IActionResult Community()
        {
            var posts = _movieRepository.GetAllPosts();
            return View(posts);
        }
    }
}

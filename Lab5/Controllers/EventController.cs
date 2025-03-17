using Lab5.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class EventController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public EventController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public IActionResult Event()
        {
            var events = _movieRepository.GetAllEvents();
            return View(events);
        }
    }
}
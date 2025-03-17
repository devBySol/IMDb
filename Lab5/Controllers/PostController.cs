using Microsoft.AspNetCore.Mvc;
using Lab5.Data.Repositories;
using Lab5.Data.Models;
using System;
using System.Xml.Linq;
using System.Reflection;

namespace Lab5.Controllers
{
    public class PostController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public PostController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // 게시물 세부 정보
        public IActionResult PostDetails(int id)
        {
            var post = _movieRepository.GetPostById(id);
            if (post == null)
            {
                return NotFound("Post not found.");
            }

            var comments = _movieRepository.GetCommentsByPostId(id).ToList();

            var viewModel = new PostDetails
            {
                Post = post,
                Comments = comments
            };

            return View(viewModel);
        }

        // 게시글 작성 (GET)
        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }

        // 게시글 작성 (POST)
        [HttpPost]
        public IActionResult AddPost(Post model)
        {
            if (ModelState.IsValid)
            {
                _movieRepository.AddPost(model);
                return RedirectToAction("Community", "Community");
            }
            return View(model);
        }


        // 댓글 작성 (POST)
        [HttpPost]
        public IActionResult AddComment([FromServices] IMovieRepository repo, CommentInputModel commentInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            var comment = new Comment
            {
                PostId = commentInput.PostId,
                UserName = commentInput.UserName,
                Content = commentInput.Content,
                DatePosted = DateTime.Now  
            };
            _movieRepository.AddComment(comment);
            return RedirectToAction("PostDetails", new { id = comment.PostId });
        }

    }
}

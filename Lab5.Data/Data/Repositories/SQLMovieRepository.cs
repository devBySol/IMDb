using Lab5.Data.Models;

namespace Lab5.Data.Repositories { 

public class SQLMovieRepository : IMovieRepository
{
    private readonly AppDBContext _context;

    public SQLMovieRepository(AppDBContext context)
    {
        _context = context;
    }

    public IEnumerable<Post> GetAllPosts() => _context.Posts.ToList();

    public Post GetPostById(int id) => _context.Posts.FirstOrDefault(p => p.Id == id);

    public void AddPost(Post post)
    {
        post.DatePosted = DateTime.Now;
        _context.Posts.Add(post);
        _context.SaveChanges();
    }

    public IEnumerable<Comment> GetCommentsByPostId(int postId) =>
        _context.Comments.Where(c => c.PostId == postId).ToList();

    public void AddComment(Comment comment)
    {
        if (comment.PostId == 0)
        {
            throw new ArgumentException("PostId is missing. Cannot add comment.");
        }

        comment.DatePosted = DateTime.Now;
        _context.Comments.Add(comment);
        _context.SaveChanges();
    }


    public IEnumerable<Event> GetAllEvents() => _context.Events.ToList();
}
}
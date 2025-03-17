//using Lab5;
//using Lab5.Data.Models;
//using Lab5.Data.Repositories;

//public class MockMovieRepository : IMovieRepository
//{

//    private List<Post> _posts;
//    private List<Comment> _comments;
//    private List<Event> _events;
//    public MockMovieRepository()
//    {

//        _posts = new List<Post>
//            {
//                new Post { Id = 1, UserName = "Alice", Title = "What's your favorite movie?", Content = "Hey everyone! 🎬\r\nI’m curious—what’s your all-time favorite movie and why? For me, it’s Inception. The mind-bending concept, the stunning visuals, and Hans Zimmer’s incredible soundtrack make it an unforgettable experience.\r\n\r\nDo you prefer classics like The Godfather, action-packed films like The Dark Knight, or maybe something more heartwarming like Forrest Gump? Let’s discuss!\r\n\r\n", DatePosted = DateTime.Now.AddDays(-2) },
//                new Post { Id = 2, UserName = "Bob", Title = "Best Action Films?", Content = "Looking for recommendations!", DatePosted = DateTime.Now.AddDays(-1) }
//            };
//        _comments = new List<Comment>
//            {
//            new Comment { Id = 1, PostId = 1, UserName = "Alice", Content = "Great discussion!", DatePosted = DateTime.Now.AddDays(-1) },
//            new Comment { Id = 2, PostId = 1, UserName = "Bob", Content = "I totally agree!", DatePosted = DateTime.Now.AddHours(-5) },
//            new Comment { Id = 3, PostId = 2, UserName = "Charlie", Content = "I love action movies!", DatePosted = DateTime.Now.AddHours(-2)
//            }


//           };

//        _events = new List<Event>
//        {
//            new Event { Id = 1, Link = "https://www.cineplex.com/events/family", Poster="https://www.cineplex.com/_next/image?url=https%3A%2F%2Fmediafiles.cineplex.com%2Fmodernization%2F23-0680_FamFavs_DesktopNormal_EN_20240223183018_0.jpg&w=1080&q=100" },
//            new Event { Id = 2, Link = "https://www.cineplex.com/events/arts-culture#The-Met%3A-Live-in-HD-2024-2025",Poster="https://www.cineplex.com/_next/image?url=https%3A%2F%2Fmediafiles.cineplex.com%2Fcineplex-v2%2Fhomepage-billboard%2F25-0020-EVCN-Met%2520Opera%20Owned%20Assets%202025-Fidelio-Desktop%20Normal_EN_20250214015542_0.jpg&w=1080&q=100" },
//            new Event { Id =3, Link="https://www.cineplex.com/movie/bonnie-clyde-the-musical", Poster="https://www.cineplex.com/_next/image?url=https%3A%2F%2Fmediafiles.cineplex.com%2Fcineplex-v2%2Fhomepage-billboard%2FBonnie%2520Clyde_EN_Desktop_Normal_20250203201610_0.jpg&w=1080&q=100"},
//            new Event {Id=4, Link="https://www.cineplex.com/movie/dr-strangelove-national-theatre-live", Poster="https://www.cineplex.com/_next/image?url=https%3A%2F%2Fmediafiles.cineplex.com%2Fcineplex-v2%2Fhomepage-billboard%2F25-0021-EVCN-Staged%2520Owned%20Assets%202025-Dr%20Strangelove-Desktop_Normal_EN_20250226212037_0.jpg&w=1080&q=100"}

//        };


//    }


//    //게시글관련
//    public IEnumerable<Post> GetAllPosts() => _posts ?? new List<Post>();
//    public Post GetPostById(int id)
//    {
//        return _posts.FirstOrDefault(p => p.Id == id);
//    }

//    public void AddPost(Post post)
//    {
//        post.Id = _posts.Count + 1;
//        post.DatePosted = DateTime.Now;
//        _posts.Add(post);
//    }

//    // 댓글 관련
//    public IEnumerable<Comment> GetCommentsByPostId(int postId) => _comments.Where(c => c.PostId == postId).ToList();
//    public void AddComment(Comment comment)
//    {
//        comment.Id = _comments.Any() ? _comments.Max(c => c.Id) + 1 : 1;
//        comment.DatePosted = DateTime.Now;
//        _comments.Add(comment);

//        var post = _posts.FirstOrDefault(p => p.Id == comment.PostId);
//        if (post != null)
//        {
//            post.Comments.Add(comment);
//        }
//    }

//    // 이벤트 관련
//    public IEnumerable<Event> GetAllEvents() => _events ?? new List<Event>();

//}
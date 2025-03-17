using Lab5.Data.Models;

namespace Lab5.Data.Repositories
{
    public interface IMovieRepository
    {
     
        // 게시글 관련
        IEnumerable<Post> GetAllPosts();
        Post GetPostById(int postId); 
        void AddPost(Post post);

        // 댓글 관련 
        IEnumerable<Comment> GetCommentsByPostId(int postId);
        void AddComment(Comment comment);

        // 이벤트 관련
        IEnumerable<Event> GetAllEvents();
    }
}

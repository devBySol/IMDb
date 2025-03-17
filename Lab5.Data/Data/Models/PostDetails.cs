using System.ComponentModel.DataAnnotations;

namespace Lab5.Data.Models
{
    public class PostDetails
    {
        public Post Post { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

        public CommentInputModel CommentInput { get; set; } = new CommentInputModel();
    }

    public class CommentInputModel
    {
        [Required]
        public int PostId { get; set; }

        [Required, MaxLength(20)]

        public string UserName { get; set; }

        [Required]
        
        public string Content { get; set; }
    }
}

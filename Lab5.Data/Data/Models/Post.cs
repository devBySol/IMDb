namespace Lab5.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;  

    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required, MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DatePosted { get; set; } = DateTime.Now; 

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public Post()
        {
            DatePosted = DateTime.Now;  
        }
    }
}

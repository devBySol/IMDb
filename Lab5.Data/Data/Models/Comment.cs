namespace Lab5.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }

        [Required, MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DatePosted { get; set; } = DateTime.Now;
    }

}

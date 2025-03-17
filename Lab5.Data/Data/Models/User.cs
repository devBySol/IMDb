using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lab5.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(256)]
        public override string UserName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public override string Email { get; set; }

        [Required]
        public override string PasswordHash { get; set; }



       
    }
}

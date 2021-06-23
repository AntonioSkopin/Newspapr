using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class RegisterModel
    {
        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
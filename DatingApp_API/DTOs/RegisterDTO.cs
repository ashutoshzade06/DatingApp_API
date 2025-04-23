using System.ComponentModel.DataAnnotations;

namespace DatingApp_API
{
    public class RegisterDTO
    {
        [Required]
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}

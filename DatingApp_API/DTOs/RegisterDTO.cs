using System.ComponentModel.DataAnnotations;

namespace DatingApp_API
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [StringLength(8,MinimumLength =4)]
        [Required]
        public  string Password { get; set; }= string.Empty;
    }
}

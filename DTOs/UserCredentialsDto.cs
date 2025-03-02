using System.ComponentModel.DataAnnotations;
using UserPermissionsApi.Models;

namespace UserPermissionsApi.DTOs
{
    public class UserCredentialsDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [PasswordMustContainNumber]
        public string Password { get; set; } = null!;
    }
}

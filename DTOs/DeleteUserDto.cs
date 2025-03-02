using System.ComponentModel.DataAnnotations;

namespace UserPermissionsApi.DTOs
{
    public class DeleteUserDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Repeat password is required")]
        public string RepeatPassword { get; set; } = null!;

    }
}

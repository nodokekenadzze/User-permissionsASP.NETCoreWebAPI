using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserPermissionsApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [PasswordMustContainNumber]
        public string Password { get; set; } = null!;

        [JsonIgnore]
        public List<UserPermission> UserPermissions { get; set; } = new();
    }
}
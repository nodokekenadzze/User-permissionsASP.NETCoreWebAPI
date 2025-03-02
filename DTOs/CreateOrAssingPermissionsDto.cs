using System.ComponentModel.DataAnnotations;

namespace UserPermissionsApi.Dtos
{
    public class CreateOrAssingPermissionsDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Permission is required")]
        public string PermissionName { get; set; } = null!;
    }
}

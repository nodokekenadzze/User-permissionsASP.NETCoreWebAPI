using System.ComponentModel.DataAnnotations;

namespace UserPermissionsApi.DTOs
{
    public class CreatePermissionDto
    {
        [Required(ErrorMessage = "Permission name is required")]
        public string Name { get; set; } = null!;
    }
}

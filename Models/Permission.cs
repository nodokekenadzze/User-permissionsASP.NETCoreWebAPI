using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserPermissionsApi.Models
{
    public class Permission
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "required field")]
        public string Name { get; set; } = null!;

        [JsonIgnore]
        public List<UserPermission> UserPermissions { get; set; } = new();
    }
}
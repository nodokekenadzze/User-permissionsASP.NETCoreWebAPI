using UserPermissionsApi.DTOs;

namespace UserPermissionsApi.Services
{
    public interface IPermissionService
    {
        Task<ServiceResponseDto> CreatePermissionAsync(string permissionName);
        Task<ServiceResponseDto> AssignPermissionAsync(string username, string permissionName);
        Task<ServiceResponseDto> RemovePermissionAsync(string username, string permissionName);
    }
}

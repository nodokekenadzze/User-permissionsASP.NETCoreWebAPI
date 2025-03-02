using UserPermissionsApi.DTOs;

namespace UserPermissionsApi.Services
{
    public interface IUserService
    {
        Task<ServiceResponseDto> CreateUserAsync(string username, string password);
        Task<ServiceResponseDto> DeleteUserAsync(string username, string password, string repeatPassword);
        Task<ServiceResponseDto> AuthorizeUserAsync(string userName, string password);
    }
}

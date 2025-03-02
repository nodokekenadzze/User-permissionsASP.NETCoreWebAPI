using Microsoft.EntityFrameworkCore;
using UserPermissionsApi.Data;
using UserPermissionsApi.Dtos;
using UserPermissionsApi.DTOs;
using UserPermissionsApi.Models;

namespace UserPermissionsApi.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly AppDbContext _context;

        public PermissionService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ServiceResponseDto> CreatePermissionAsync(string permissionName)
        {
            var existingPermission = await _context.Permissions
                .FirstOrDefaultAsync(p => p.Name == permissionName);

            if (existingPermission != null)
            {
                return new ServiceResponseDto
                {
                    Success = false,
                    Message = $"Permission '{permissionName}' already exists"
                };
            }

            var permission = new Permission { Name = permissionName };
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();

            return new ServiceResponseDto
            {
                Success = true,
                Message = $"Permission '{permissionName}' created successfully with ID {permission.Id}"
            };
        }

        public async Task<ServiceResponseDto> AssignPermissionAsync(string username, string permissionName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.Name == permissionName);

            if (user == null)
                return new ServiceResponseDto { Success = false, Message = $"User '{username}' not found" };
            if (permission == null)
                return new ServiceResponseDto { Success = false, Message = $"Permission '{permissionName}' not found" };

            var existingAssignment = await _context.UserPermissions
                .AnyAsync(up => up.UserId == user.Id && up.PermissionId == permission.Id);

            if (existingAssignment)
                return new ServiceResponseDto { Success = false, Message = $"Permission '{permissionName}' already assigned to '{username}'" };

            var userPermission = new UserPermission { UserId = user.Id, PermissionId = permission.Id };
            await _context.UserPermissions.AddAsync(userPermission);
            await _context.SaveChangesAsync();

            return new ServiceResponseDto
            {
                Success = true,
                Message = $"Permission '{permissionName}' successfully assigned to '{username}'"
            };
        }

        public async Task<ServiceResponseDto> RemovePermissionAsync(string username, string permissionName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.Name == permissionName);

            if (user == null)
                return new ServiceResponseDto { Success = false, Message = $"User '{username}' not found" };
            if (permission == null)
                return new ServiceResponseDto { Success = false, Message = $"Permission '{permissionName}' not found" };

            var userPermission = await _context.UserPermissions
                .FirstOrDefaultAsync(up => up.UserId == user.Id && up.PermissionId == permission.Id);

            if (userPermission == null)
                return new ServiceResponseDto { Success = false, Message = $"User '{username}' does not have permission '{permissionName}'" };

            _context.UserPermissions.Remove(userPermission);
            await _context.SaveChangesAsync();

            return new ServiceResponseDto
            {
                Success = true,
                Message = $"Permission '{permissionName}' successfully removed from '{username}'"
            };
        }
    }
}
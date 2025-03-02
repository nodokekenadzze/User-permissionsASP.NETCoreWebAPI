using Microsoft.EntityFrameworkCore;
using UserPermissionsApi.Data;
using UserPermissionsApi.Models;
using UserPermissionsApi.DTOs;
//using BCrypt.Net;


namespace UserPermissionsApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ServiceResponseDto> CreateUserAsync(string username, string password)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (existingUser != null)
                return new ServiceResponseDto
                {
                    Success = false,
                    Message = $"Username '{username}' already exists"
                };
            

            //var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User { Username = username, Password = password };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ServiceResponseDto
            {
                Success = true,
                Message = $"User '{username}' created successfully with ID {user.Id}"
            };
        }

        public async Task<ServiceResponseDto> AuthorizeUserAsync(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return new ServiceResponseDto { Success = false, Message = $"User '{username}' not found" };
            }
            //if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            if (user.Password != password)
            {
                return new ServiceResponseDto { Success = false, Message = "Incorrect password" };
            }

            return new ServiceResponseDto
            {
                Success = true,
                Message = $"Welcome to dotnet world {username}!"
            };
        }

        public async Task<ServiceResponseDto> DeleteUserAsync(string userName, string password, string repeatPassword)
        {
            var user = await _context.Users.
                FirstOrDefaultAsync(u => u.Username == userName);


            if (user == null)        
                return new ServiceResponseDto
                {
                    Success = false,
                    Message = $"User {user} does not exist"
                };
            

            if (user.Password != password)
                return new ServiceResponseDto
                {
                    Success = false,
                    Message = "Incorrect password"
                };

            if (password != repeatPassword)
                return new ServiceResponseDto
                {
                    Success = false,
                    Message = "Passwords does not match"
                };

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new ServiceResponseDto
            {
                Success = true,
                Message = $"User '{userName}' deleted successfully"
            };
        }
    }
}
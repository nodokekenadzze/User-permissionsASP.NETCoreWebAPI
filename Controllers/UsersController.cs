using Microsoft.AspNetCore.Mvc;
using UserPermissionsApi.Services;
using UserPermissionsApi.DTOs;

namespace UserPermissionsApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCredentialsDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _service.CreateUserAsync(request.Username, request.Password);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> AuthorizeUser(UserCredentialsDto request)
        {
            var response = await _service.AuthorizeUserAsync(request.Username, request.Password);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(DeleteUserDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.DeleteUserAsync(
                request.UserName,
                request.Password,
                request.RepeatPassword
            );
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
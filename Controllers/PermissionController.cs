using Microsoft.AspNetCore.Mvc;
using UserPermissionsApi.Dtos;
using UserPermissionsApi.DTOs;
using UserPermissionsApi.Models;
using UserPermissionsApi.Services;

namespace UserPermissionsApi.Controllers
{
    [Route("api/permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service)
        {
            _service = service;
        }

        [HttpPost("name")]
        public async Task<IActionResult> CreatePermission(CreatePermissionDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _service.CreatePermissionAsync(request.Name);
            return response.Success ? Ok(response.Message) : BadRequest(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AssignPermission(CreateOrAssingPermissionsDto request)
        {
            var response = await _service.AssignPermissionAsync(request.Username, request.PermissionName);
            return response.Success ? Ok(response.Message) : BadRequest(response.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> RemovePermission(CreateOrAssingPermissionsDto request)
        {
            var response = await _service.RemovePermissionAsync(request.Username, request.PermissionName);
            return response.Success ? Ok(response.Message) : BadRequest(response.Message);
        }
    }
}
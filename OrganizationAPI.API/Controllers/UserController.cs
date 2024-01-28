using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizationAPI.API.Configurations;
using OrganizationAPI.Domain.Abstractions.DTOs.Authentication;
using OrganizationAPI.Domain.Abstractions.DTOs.User;
using OrganizationAPI.Domain.Abstractions.Enums;
using OrganizationAPI.Domain.Abstractions.Services;

namespace OrganizationAPI.API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            try
            {
                var users = await userService.GetAll();
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            try
            {
                var user = await userService.GetById(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<UserDto>> Create(RegisterDto register)
        {
            try
            {
                UserDto user = mapper.Map<UserDto>(register);
                await userService.Add(new List<UserDto>
                {
                    user
                });

                return Ok(register);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<UserDto>> Update(UserDto user)
        {
            try
            {
                await userService.Update(user);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(Role.Admin)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await userService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }
    }
}

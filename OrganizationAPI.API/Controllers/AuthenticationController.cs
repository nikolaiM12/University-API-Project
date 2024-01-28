using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrganizationAPI.Domain.Abstractions.DTOs.Authentication;
using OrganizationAPI.Domain.Abstractions.Services;

namespace OrganizationAPI.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto account)
        {
            var token = await authenticationService.Login(account);

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            var registeredUser = await authenticationService.Register(register);

            if (registeredUser != null)
            {
                var registrationJson = JsonConvert.SerializeObject(registeredUser, Formatting.Indented);
                return Ok(registrationJson);
            }
            else
            {
                return NoContent();
            }
        }
    }
}

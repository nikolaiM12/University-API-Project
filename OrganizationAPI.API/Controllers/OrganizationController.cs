using Microsoft.AspNetCore.Mvc;
using OrganizationAPI.API.Configurations;
using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Enums;
using OrganizationAPI.Domain.Abstractions.Services;
using OrganizationAPI.Domain.Services;

namespace OrganizationAPI.API.Controllers
{
    [Route("organization")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService organizationService;
        public OrganizationController(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<OrganizationDto>>> GetAll()
        {
            try
            {
                var organizations = await organizationService.GetAll();
                return Ok(organizations);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<OrganizationDto>> GetById(int id)
        {
            try
            {
                var organization = await organizationService.GetById(id);
                return Ok(organization);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(OrganizationDto organization)
        {
            try
            {
                await organizationService.Add(new List<OrganizationDto>
                {
                    organization
                });

                return Ok(organization);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<OrganizationDto>> Update(OrganizationDto organization)
        {
            try
            {
                await organizationService.Update(organization);
                return Ok(organization);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(Role.Admin)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await organizationService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }
    }
}

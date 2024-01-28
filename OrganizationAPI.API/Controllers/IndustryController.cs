using Microsoft.AspNetCore.Mvc;
using OrganizationAPI.API.Configurations;
using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Enums;
using OrganizationAPI.Domain.Abstractions.Services;
using OrganizationAPI.Domain.Services;

namespace OrganizationAPI.API.Controllers
{
    [Route("industry")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        private readonly ISectorService sectorService;
        public IndustryController(ISectorService sectorService)
        {
            this.sectorService = sectorService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<IndustryDto>>> GetAll()
        {
            try
            {
                var industries = await sectorService.GetAll();
                return Ok(industries);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<IndustryDto>> GetById(int id)
        {
            try
            {
                var industry = await sectorService.GetById(id);
                return Ok(industry);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(IndustryDto industry)
        {
            try
            {
                await sectorService.Add(new List<IndustryDto> 
                { 
                    industry 
                });

                return Ok(industry);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<IndustryDto>> Update(IndustryDto industry)
        {
            try
            {
                await sectorService.Update(industry);
                return Ok(industry);
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
                await sectorService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }
    }
}

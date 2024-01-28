using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationAPI.Client.Domain.Model;
using OrganizationAPI.Domain.Abstractions.Services;

namespace OrganizationAPI.API.Controllers
{
    [Route("csv")]
    [ApiController]
    public class CsvDataController : ControllerBase
    {
        private readonly IOrganizationService organizationService;
        public CsvDataController(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }

        [HttpGet("details")]
        public async Task<ActionResult<List<OrganizationDataModel>>> GetOrganizationDetails()
        {
            try
            {
                var organizations = await organizationService.GetOrganizationDetails();
                return Ok(organizations);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}

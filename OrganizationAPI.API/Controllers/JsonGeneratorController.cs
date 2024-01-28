using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationAPI.Domain.Abstractions.Services;

namespace OrganizationAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JsonGeneratorController : ControllerBase
    {
        private readonly IJsonGeneratorService jsonGeneratorService;
        private readonly IStatisticsService statisticsService;

        public JsonGeneratorController(IJsonGeneratorService jsonGeneratorService, IStatisticsService statisticsService)
        {
            this.jsonGeneratorService = jsonGeneratorService;
            this.statisticsService = statisticsService;
        }

        [HttpPost("generate-daily-json")]
        public async Task<IActionResult> GenerateDailyJsonFile()
        {
            try
            {
                await jsonGeneratorService.CreateDailyJsonFile(statisticsService);
                return Ok("Daily JSON file generated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generating daily JSON file: {ex.Message}");
            }
        }
    }
}

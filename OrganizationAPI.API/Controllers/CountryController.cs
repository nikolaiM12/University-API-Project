using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Operators;
using OrganizationAPI.API.Configurations;
using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Enums;
using OrganizationAPI.Domain.Abstractions.Services;
using OrganizationAPI.Domain.Services;

namespace OrganizationAPI.API.Controllers
{
    [Route("country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService countryService;
        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<CountryDto>>> GetAll()
        {
            try
            {
                var countries = await countryService.GetAll();
                return Ok(countries);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<CountryDto>> GetById(int id)
        {
            try
            {
                var country = await countryService.GetById(id);
                return Ok(country);
            }
            catch(Exception ex )
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CountryDto country)
        {
            try
            {
                await countryService.Add(new List<CountryDto> 
                { 
                    country 
                });
                return Ok(country);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<CountryDto>> Update(CountryDto country)
        {
            try
            {
                await countryService.Update(country);
                return Ok(country);
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
                await countryService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }
    }
}

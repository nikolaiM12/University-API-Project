using Microsoft.AspNetCore.Mvc;
using OrganizationAPI.API.Configurations;
using OrganizationAPI.Domain.Abstractions.Enums;
using OrganizationAPI.Domain.Abstractions.Services;

namespace OrganizationAPI.API.Controllers
{
    [Route("pdf")]
    [ApiController]
    public class PdfDownloadController : ControllerBase
    {
        private readonly IPdfDownloadService pdfService;
        public PdfDownloadController(IPdfDownloadService pdfService)
        {
            this.pdfService = pdfService;
        }

        [HttpGet("downloadPdf/{organizationId}")]
        [AuthorizeRoles(Role.Admin)]
        public async Task<IActionResult> DownloadPdf(int organizationId)
        {
            try
            {
                byte[] fileBytes = await pdfService.PdfDownload(organizationId);

                string contentType = "application/pdf";

                return File(fileBytes, contentType, $"{DateTime.Now:dd-MM-yyyy}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

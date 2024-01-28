using iTextSharp.text;
using iTextSharp.text.pdf;
using OrganizationAPI.Domain.Abstractions.DTOs;
using OrganizationAPI.Domain.Abstractions.Services;

namespace OrganizationAPI.Domain.Services
{
    public class PdfDownloadService : IPdfDownloadService
    {
        private readonly IOrganizationService organizationService;
        public PdfDownloadService(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }
        public async Task<byte[]> PdfDownload(int id)
        {
            try
            {
                OrganizationDto organization = await organizationService.GetById(id);

                using (var ms = new MemoryStream())
                {
                    using (var document = new Document())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(document, ms);

                        document.Open();

                        document.Add(new Paragraph($"Organization ID: {organization.OrganizationId}"));
                        document.Add(new Paragraph($"Name: {organization.Name}"));
                        document.Add(new Paragraph($"Website: {organization.Website}"));
                        document.Add(new Paragraph($"Country ID: {organization.CountryId}"));
                        document.Add(new Paragraph($"Description: {organization.Description}"));
                        document.Add(new Paragraph($"Founded: {organization.Founded}"));
                        document.Add(new Paragraph($"Industry ID: {organization.IndustryId}"));
                        document.Add(new Paragraph($"Number of Employees: {organization.NumberOfEmployees}"));
                        document.Add(new Paragraph($"Is Deleted: {organization.IsDeleted}"));

                        document.Close();
                        writer.Close();

                        return ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}

namespace OrganizationAPI.Domain.Abstractions.Services
{
    public interface IPdfDownloadService
    {
        Task<byte[]> PdfDownload(int id);
    }
}

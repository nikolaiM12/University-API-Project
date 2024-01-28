namespace OrganizationAPI.Client.Domain.Abstractions.Services
{
    public interface IFileSystemService
    {
        void EnsureDirectoryExists(string directoryPath);
        void MoveFileIfNotExists(string sourceFilePath, string destinationFilePath);
        List<string> GetFilesInDirectory(string directoryPath);
    }
}

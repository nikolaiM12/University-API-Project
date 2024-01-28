using OrganizationAPI.Client.Domain.Abstractions.Services;

namespace OrganizationAPI.Domain.Services
{
    public class FileSystemService : IFileSystemService
    {
        public void EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public void MoveFileIfNotExists(string sourceFilePath, string destinationFilePath)
        {
            if (File.Exists(sourceFilePath) && !File.Exists(destinationFilePath))
            {
                File.Move(sourceFilePath, destinationFilePath);
            }
        }

        public List<string> GetFilesInDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                return Directory.GetFiles(directoryPath).ToList();
            }
            return new List<string>();
        }
    }
}
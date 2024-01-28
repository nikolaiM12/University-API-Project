using CsvHelper;
using OrganizationAPI.Client.Domain.Abstractions.Configurations;
using OrganizationAPI.Client.Domain.Abstractions.Services;
using OrganizationAPI.Client.Domain.Model;

namespace OrganizationAPI.Domain.Services
{
    public class CsvParsingService : ICsvParsingService
    {
        public async Task<List<OrganizationDataModel>> ParseCsvFile()
        {
            try
            {
                FileSystemService fileSystemService = new FileSystemService();
                string inputDirectory = FilePathsConfiguration.CsvDirectoryPath;
                string outputDirectory = FilePathsConfiguration.DailyCsvOutputDirectoryPath;

                fileSystemService.EnsureDirectoryExists(outputDirectory);

                var csvFiles = fileSystemService.GetFilesInDirectory(inputDirectory);

                List<OrganizationDataModel> records = new List<OrganizationDataModel>();

                foreach (var file in csvFiles)
                {
                    using (var reader = new StreamReader(file))
                    using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        records.AddRange(csv.GetRecords<OrganizationDataModel>());
                    }

                    // Move file after parsing
                    string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
                    string destinationFilePath = Path.Combine(outputDirectory, $"{currentDate}.csv");

                    fileSystemService.MoveFileIfNotExists(file, destinationFilePath);
                }

                return records;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while parsing CSV: {ex.Message}");
            }
        }
    }
}
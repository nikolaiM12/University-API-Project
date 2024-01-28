namespace OrganizationAPI.Client.Domain.Abstractions.Configurations
{
    public static class FilePathsConfiguration
    {
        //public static string DailyCsvOutputFilePath => $"C:\\Users\\USER\\Desktop\\Read\\{DateTime.Now:dd-MM-yyyy}.csv";
        public static string DailyCsvOutputDirectoryPath => $"C:\\Users\\USER\\Desktop\\Read";
        public static string ExcelFilePath => "C:\\Users\\USER\\Desktop\\Xlsx";
        public static string CsvDirectoryPath => "C:\\Users\\USER\\Desktop\\CSV";
    }
}

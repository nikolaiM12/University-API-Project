namespace DemoVersion.API.Abstractions.DTOs.Statistics
{
    public class TotalEmployeesByCountryAndIndustryDto
    {
        public int CountryId { get; set; }
        public int IndustryId { get; set; }
        public int TotalEmployees { get; set; }
    }
}

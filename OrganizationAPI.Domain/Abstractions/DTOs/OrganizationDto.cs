namespace OrganizationAPI.Domain.Abstractions.DTOs
{
    public class OrganizationDto
    {
        public int Id { get; set; }
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public int CountryId { get; set; }
        public string Description { get; set; }
        public int Founded { get; set; }
        public int IndustryId { get; set; }
        public int NumberOfEmployees { get; set; }
        public bool IsDeleted { get; set; }
    }
}

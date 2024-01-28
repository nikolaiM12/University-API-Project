using CsvHelper.Configuration.Attributes;

namespace OrganizationAPI.Client.Domain.Model
{
    public class OrganizationDataModel : OrganizationDataModelBase
    {
        public int Index { get; set; }

        [Name("Organization Id")]
        public string OrganizationId { get; set; }

        public string Name { get; set; }

        public string Website { get; set; }

        public string Country { get; set; }

        public string Description { get; set; }

        public int Founded { get; set; }

        public string Industry { get; set; }

        [Name("Number of employees")]
        public int NumberOfEmployees { get; set; }
        public override string GetDetails()
        {
            return $"Index: {Index}\n" +
                   $"OrganizationId: {OrganizationId}\n" +
                   $"Name: {Name}\n" +
                   $"Website: {Website}\n" +
                   $"Country: {Country}\n" +
                   $"Description: {Description}\n" +
                   $"Founded: {Founded}\n" +
                   $"Industry: {Industry}\n" +
                   $"NumberOfEmployees: {NumberOfEmployees}";
        }
    }
}

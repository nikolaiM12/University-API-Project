namespace OrganizationAPI.Domain.Abstractions.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password, out string salt);
        bool VerifyPassword(string password, string salt, string hashedPassword);
    }
}

using Hash = BCrypt.Net.BCrypt;

namespace RealEstateAgency.Shared.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
            => Hash.HashPassword(password);

        public bool Verify(string password, string hash)
            => Hash.Verify(password, hash);

        public bool VerifyUser(string email, string password, string hash)
            => Hash.Verify(HashPair(email, password), hash);

        public string HashUserPassword(string email, string password)
            => Hash.HashPassword(HashPair(email, password));

        private string HashPair(string email, string password)
            => $"{email}_{password}";
    }

    public interface IPasswordService
    {
        string HashPassword(string password);
        string HashUserPassword(string email, string password);
        bool Verify(string password, string hash);
        bool VerifyUser(string email, string password, string hash);

    }
}

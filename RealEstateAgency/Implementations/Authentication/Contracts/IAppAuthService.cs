using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.RBAC;
using RealEstateAgency.Dtos.Other.Auth;

namespace RealEstateAgency.Implementations.Authentication.Contracts
{
    public interface IAppAuthService
    {
        Task<UserAccountDto> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<UserAccountDto> RegisterAgentAsync(RegisterAgentDto registerAgentDto, CancellationToken cancellationToken);
        Task<UserAccountDto> RegisterAsync(RegisterUserDto registerDto, CancellationToken cancellationToken);
        Task CheckIfUserIsValid(string email, string username, CancellationToken cancellationToken);
        Task<UserAccount> UpdatePasswordAsync(int userId, string newPassword);
    }

}

using DevFreela.Core.Entities;

namespace DevFreela.Core.Interfaces
{
    public interface IAuthenticationService
    {
        string GenerateJwtToken(User user);
    }
}

namespace DevFreela.Core.Interfaces
{
    public interface IPasswordResetService
    {
        Task SendPasswordResetCodeAsync(string email, CancellationToken cancellationToken = default);
        bool ValidatePasswordResetCode(string email, string resetCode);
    }
}

using DevFreela.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.Infrastructure.Security
{
    public class PasswordResetService(IMemoryCache cache, IEmailService emailService) : IPasswordResetService
    {
        private static string FormatCacheKey(string email) => $"ResetCode:{email}";

        public async Task SendPasswordResetCodeAsync(string email, CancellationToken cancellationToken = default)
        {
            var code = Random.Shared.Next(100000, 999999).ToString();
            cache.Set(FormatCacheKey(email), code, TimeSpan.FromMinutes(10));
            await emailService.SendAsync(email, "Password Reset Code", $"Your password reset code is: {code}", cancellationToken);
        }

        public bool ValidatePasswordResetCode(string email, string resetCode)
        {
            if (!cache.TryGetValue(FormatCacheKey(email), out string? value))
            {
                return false;
            }

            return value == resetCode;
        }
    }
}

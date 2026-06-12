using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CoffeeShop.Services
{
    public class NullEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // no-op for development
            return Task.CompletedTask;
        }
    }
}

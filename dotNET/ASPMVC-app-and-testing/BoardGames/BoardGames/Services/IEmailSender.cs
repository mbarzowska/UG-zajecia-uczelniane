using System.Threading.Tasks;

namespace BoardGames.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}

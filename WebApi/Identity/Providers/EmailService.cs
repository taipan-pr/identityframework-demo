using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace WebApi.Identity.Providers
{
    internal class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            using (var mailClient = new SmtpClient())
            {
                var email = new MailMessage
                {
                    From = new MailAddress("no-reply@identitydemo.com"),
                    To = { new MailAddress(message.Destination) },
                    Subject = message.Subject,
                    Body = message.Body
                };

                await mailClient.SendMailAsync(email);
            }
        }
    }
}

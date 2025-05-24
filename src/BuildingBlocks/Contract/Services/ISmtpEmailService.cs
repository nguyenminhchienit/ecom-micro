using Shared.Services.Email;

namespace Contract.Services
{
    public interface ISmtpEmailService : IEmailService<MailRequest>
    {

    }
}

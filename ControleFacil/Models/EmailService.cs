using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace ControleFacil.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarEmailAsync(string para, string assunto, string mensagem)
        {
            var smtp = new SmtpClient(_config["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_config["EmailSettings:SmtpPort"]),
                Credentials = new NetworkCredential(_config["EmailSettings:Email"], _config["EmailSettings:Senha"]),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_config["EmailSettings:Email"], "Controle Fácil"),
                Subject = assunto,
                Body = mensagem,
                IsBodyHtml = true
            };

            mail.To.Add(para);

            await smtp.SendMailAsync(mail);
        }
    }
}

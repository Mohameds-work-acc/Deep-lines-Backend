using Deep_lines_Backend.BLL.DTOs.EmailDTOs;
using Deep_lines_Backend.BLL.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace Deep_lines_Backend.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
        }

        public async Task sendEmail(sendEmailDTO sendEmailDTO)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(emailSettings.Email));
            email.To.Add(MailboxAddress.Parse(sendEmailDTO.Email));
            email.Subject = sendEmailDTO.Subject;

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = sendEmailDTO.Body
            };

            using var smpt = new SmtpClient();

            await smpt.ConnectAsync(emailSettings.Host, emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);

            await smpt.AuthenticateAsync(emailSettings.Email, emailSettings.Password);

            await smpt.SendAsync(email);
            await smpt.DisconnectAsync(true);

        }
    }
}

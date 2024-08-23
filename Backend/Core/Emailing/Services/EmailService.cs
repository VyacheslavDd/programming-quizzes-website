using Core.Emailing.Models;
using Core.Emailing.Options;
using MailKit.Net.Smtp;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Emailing.Services
{
	internal class EmailService : IEmailService
	{
		private readonly MailOptions _mailOptions;

		public EmailService(IOptions<MailOptions> mailOptions)
		{
			_mailOptions = mailOptions.Value;
		}
		public async Task SendSimpleEmailAsync(SimpleEmail email)
		{
			var mail = new MimeMessage();
			mail.Sender = MailboxAddress.Parse(_mailOptions.Mail);
			mail.To.Add(MailboxAddress.Parse(email.To));
			mail.Subject = email.Subject;
			var builder = new BodyBuilder();
			builder.TextBody = email.Body;
			mail.Body = builder.ToMessageBody();
			using (var smtpClient = new SmtpClient())
			{
				await smtpClient.ConnectAsync(_mailOptions.Host, _mailOptions.Port);
				await smtpClient.AuthenticateAsync(_mailOptions.Username, _mailOptions.Password);;
				await smtpClient.SendAsync(mail);
				await smtpClient.DisconnectAsync(true);
			}
		}
	}
}

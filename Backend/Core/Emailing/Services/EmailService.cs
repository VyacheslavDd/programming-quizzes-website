using Core.CommonFunctions;
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

		public async Task SendConfirmationEmailAsync(string userLogin, string to, string sequence)
		{
			var mail = new SimpleEmail();
			mail.To = to;
			mail.Subject = "Завершение регистрации на сайте Quizz";
			mail.Body = $@"{userLogin}, вы регистрировались на сайте Quizz. Завершите регистрацию, пройдя по ссылке:
			http://localhost:5173/confirm-email/{sequence}";
			await SendSimpleEmailAsync(mail);
		}

		public async Task SendNewQuizPublishedEmailAsync(string userLogin, string to, string quizTitle, string languageCategory, string quizId)
		{
			var mail = new SimpleEmail();
			mail.To = to;
			mail.Subject = "Quizz: новая викторина!";
			mail.Body = $"{userLogin}, на сайте появилась новая викторина {quizTitle} ({languageCategory})<br>" +
				$"<a href='http://localhost:5173/quizzes/{quizId}'>Решить тест</a>";
			await SendSimpleEmailAsync(mail);
		}

		public async Task SendRegistrationFinishedEmailAsync(string userLogin, string to)
		{
			var mail = new SimpleEmail();
			mail.To = to;
			mail.Subject = "Quizz: регистрация завершена";
			mail.Body = $"{userLogin}, регистрация завершена успешно. Теперь вам доступен весь функционал.";
			await SendSimpleEmailAsync(mail);
		}

		public async Task SendSimpleEmailAsync(SimpleEmail email)
		{
			var mail = new MimeMessage();
			mail.Sender = MailboxAddress.Parse(_mailOptions.Mail);
			mail.To.Add(MailboxAddress.Parse(email.To));
			mail.Subject = email.Subject;
			var builder = new BodyBuilder();
			builder.HtmlBody = email.Body;
			mail.Body = builder.ToMessageBody();
			using (var smtpClient = new SmtpClient())
			{
				await smtpClient.ConnectAsync(_mailOptions.Host, _mailOptions.Port);
				await smtpClient.AuthenticateAsync(_mailOptions.Username, EmailingHelper.ReadSmtpPassword());
				await smtpClient.SendAsync(mail);
				await smtpClient.DisconnectAsync(true);
			}
		}
	}
}

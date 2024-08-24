using Core.Emailing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Emailing.Services
{
	public interface IEmailService
	{
		Task SendSimpleEmailAsync(SimpleEmail email);
		Task SendConfirmationEmailAsync(string userLogin, string to, string sequence);
	}
}

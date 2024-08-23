using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Emailing.Models
{
    public abstract class Email
	{
		public virtual string To { get; set; }
		public virtual string Subject { get; set; }
		public virtual string Body { get; set; }
	}
}

using IB_Student_Manager.Models;
using MailKit.Net.Smtp;
using MimeKit;
using Org.BouncyCastle.Crypto.Macs;
using System.ComponentModel.DataAnnotations;

namespace IB_Student_Manager
{
	public class Mailkit
	{
		public Mailkit()
		{

		}
		public void SendEmail(Email EmailInfo)
		{
			using (var client = new SmtpClient())
			{
				client.Connect("smtp.gmail.com", 465);

				////Note: only needed if the SMTP server requires authentication
				client.Authenticate("ibstudentmanagerserver@gmail.com", "kxgnckrxvkrbmbmq");


				var message = new MimeMessage();
				//Senders Name + Senders Email
				message.From.Add(new MailboxAddress(EmailInfo.SenderName, EmailInfo.SenderEmail));
				//Recipient Email
				message.To.Add(new MailboxAddress(EmailInfo.RecipientName, EmailInfo.RecipientEmail));
				//Subject
				message.Subject = EmailInfo.Subject;
				//Body / Text
				message.Body = new TextPart("plain") { Text = EmailInfo.Body };


				client.Send(message);
				client.Disconnect(true);
			}

		}
	}
}

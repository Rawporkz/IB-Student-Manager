using MailKit.Net.Smtp;
using MimeKit;

namespace IB_Student_Manager
{
    public class Mailkit
    {

        public void SendEmail()
        {
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);

                ////Note: only needed if the SMTP server requires authentication
                //client.Authenticate(Email, password); //email and password of who is the sender

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("Techathon Hospital", "TechathonHospital@gmail.com"));//sender name , email
                message.To.Add(new MailboxAddress("ADVICE 101", "phurich.amorn@gmail.com"));// email of person recieving 
                message.Subject = "ADVICE 101"; //subject
                message.Body = new TextPart("plain") { Text = @"Hospital Appoinment at xx:xx at DD/mm/YYYY " }; //text
                client.Send(message);

                client.Disconnect(true);
            }
        }
    }
}

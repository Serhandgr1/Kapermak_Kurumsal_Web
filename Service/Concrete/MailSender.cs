using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class MailSender
    {
        private static string senderEmail = "mail göndercek hesap";
        private static string senderPassword = "hesap şifresi";
        public async void SendMail(string receiver,  string subject, string message)
        {
            using (var client = new SmtpClient("smtp.gmail.com"))
            {
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                var mail = new MailMessage(senderEmail, receiver, subject, message);

                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}

using System.Net;
using System.Net.Mail;

namespace _8__AutenticaEAutorizaIdentityAPI.Services
{
    public class EmailServices
    {
        public bool Send(string toName, string toEmail, string subject, string body, string fromName = "equipe patrick", string fromEmail = "patrickaquinodesouza@gmail.com")   
        {
            var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port);

            smtpClient.Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            var mail = new MailMessage();

            mail.From = new MailAddress(fromEmail, fromName);
            mail.To.Add(new MailAddress(toEmail, toName));
            //mail.To.Add(new MailAddress(toName, toEmai)); //pode ter varios
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true; //<h1> 
            try
            {
                smtpClient.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}

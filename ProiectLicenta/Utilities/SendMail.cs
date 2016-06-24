using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProiectLicenta.Utilities
{
    public class Mail
    {
        /*
         * parametru - adresa la care se doreste a fi trimis mesajul
         * */
        public static async void SendMail(string to)
        {
            string body = "Ati fost adugat in echipa de organizare a unui eveniment";
            MailMessage message = new MailMessage();
            message.To.Add(to);
            message.From = new MailAddress("cosmin.popescu93@gmail.com");
            message.Subject = "Mesaj de la server";
            message.Body = body;

            using (SmtpClient smtp = new SmtpClient())
            {
                NetworkCredential credentials = new NetworkCredential
                {
                    UserName = "cosmin.popescu93@gmail.com",
                    Password = "cosminpop"
                };

                smtp.Credentials = credentials;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }
    }
}
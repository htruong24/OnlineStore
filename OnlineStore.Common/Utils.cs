using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Common
{
    public static class Utils
    {
        public static string ToCurrency(this decimal? price)
        {
            var convertPrice = price ?? 0;
            return convertPrice.ToString("N0").Replace(',', '.');
        }

        public static string ToCurrency(this decimal price)
        {
            return price.ToString("N0").Replace(',', '.');
        }

        public static string SendMail(string toList, string from, string fromTitle, string ccList, string subject, string body)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            try
            {
                MailAddress fromAddress = new MailAddress(from, fromTitle, Encoding.UTF8);
                message.From = fromAddress;
                message.To.Add(toList);
                if (ccList != null && ccList != string.Empty)
                    message.CC.Add(ccList);

                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                smtpClient.Host = "smtp.gmail.com";   // We use gmail as our smtp client
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential("nexmitesting@gmail.com", "@Dmin123");
                smtpClient.Send(message);

                msg = "Successful";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
    }
}

using System;

using System.Net;
using System.Net.Mail;

namespace Roomie.CommandDefinitions.EmailCommands
{
    public class Mailer
    {
        System.Net.Mail.SmtpClient smtpClient;

        public string DefaultFromAddress;
        public string DefaultFromName;

        #region default settings for common servers
        public static Mailer CreateGmail(string username, string password)
        {
            Mailer toReturn = new Mailer("smtp.gmail.com", 587, true, username, password);
            toReturn.DefaultFromAddress = username;
            return toReturn;
        }
        public static Mailer CreateLiveMail(string username, string password)
        {
            //smtp could also be 587
            Mailer toReturn = new Mailer("smtp.live.com", 25, true, username, password);
            toReturn.DefaultFromAddress = username;
            return toReturn;

        }
        public static Mailer CreateGoDaddy(string username, string password, bool secure)
        {
            if (secure)
                return CreateGoDaddySsl(username, password);
            else
                return CreateGoDaddyNoSsl(username, password);
        }
        public static Mailer CreateGoDaddySsl(string username, string password)
        {
            Mailer toReturn = new Mailer("smtpout.secureserver.net", 465, true, username, password);
            toReturn.DefaultFromAddress = username;
            return toReturn;
        }
        public static Mailer CreateGoDaddyNoSsl(string username, string password)
        {
            Mailer toReturn = new Mailer("smtpout.secureserver.net", 80, false, username, password);
            toReturn.DefaultFromAddress = username;
            return toReturn;
        }
        #endregion

        public Mailer(string host, int port, bool enableSsl, string username, string password)
        {
            smtpClient = new SmtpClient();
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = enableSsl;
            smtpClient.Credentials = new NetworkCredential(username, password);
            //smtpClient.Timeout = 10;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

        }

        #region Send Message methods
        public bool SendMessage(MailMessage message)
        {
            try
            {
                smtpClient.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SendMessage(MailAddress from, MailAddress to, string subject, string body)
        {
            MailMessage message = new MailMessage();

            message.From = from;
            message.To.Add(to);
            message.Body = body;
            message.Subject = subject;

            return SendMessage(message);
        }
        public bool SendMessage(string from, string to, string subject, string body)
        {
            return SendMessage(new System.Net.Mail.MailAddress(from), new System.Net.Mail.MailAddress(to), subject, body);
        }
        public bool SendMessage(MailAddress to, string subject, string body)
        {
            return SendMessage(DefaultFrom, to, subject, body);
        }
        public bool SendMessage(string to, string subject, string body)
        {
            return SendMessage(DefaultFrom, new MailAddress(to), subject, body);
        }
        #endregion

        public MailAddress DefaultFrom
        {
            get
            {
                if (String.IsNullOrEmpty(DefaultFromAddress))
                    return null;
                if(string.IsNullOrEmpty(DefaultFromName))
                    return new MailAddress(DefaultFromAddress);
                return new MailAddress(DefaultFromAddress, DefaultFromName);
            }
        }
    }
}

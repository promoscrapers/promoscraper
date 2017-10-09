using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace Utilities.Email
{
    public class SMTPConnectionProperties
    {
        public string SMTPServer = "send.smtp.com";
        public int SMTPPort = 2525;
        public string UserName = "mitnutra@gmail.com";
        public string Password = "b83981c2";
    }

    public class EmailClient
    {
        SMTPConnectionProperties _connectionProperties = null;

        public EmailClient()
        {
           
        }

        private SmtpClient getSmtpClientObject()
        {
            try
            {

                _connectionProperties = new SMTPConnectionProperties();
                string mailServer = _connectionProperties.SMTPServer;
                int mailPort = _connectionProperties.SMTPPort;
                string userName = _connectionProperties.UserName.ToString();
                string pass = _connectionProperties.Password.ToString();

                SmtpClient smtp = new SmtpClient(mailServer);

                if (mailPort > 0) smtp.Port = mailPort;

                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(pass))
                {
                    NetworkCredential credentials = new NetworkCredential(userName, pass);
                    smtp.Credentials = credentials;
                }

                return smtp;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public bool SendEmail(string From, string To, string Subject, string Body, bool IsBodyHtml)
        {
            try
            {
                bool success = true;

                MailMessage mailMessage = new MailMessage(From, To, Subject, Body);
                mailMessage.IsBodyHtml = IsBodyHtml;

                success = SendEmail(mailMessage);

                return success;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool SendEmail(string From, string To, string Subject, string Body)
        {
            bool success = true;

            success = SendEmail(From, To, Subject, Body, false);

            return success;
        }

        public bool SendEmail(MailMessage mailMessage)
        {
            bool success = true;

            SmtpClient smtp = getSmtpClientObject();

                       
                try
                {
                    smtp.Send(mailMessage);
                }
                catch(Exception ex) {
              success = false;
            }
           

            return success;
        }



    }    
}

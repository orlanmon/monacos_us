using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Configuration;


    namespace monacos.us_mvc.BusinessObjects
    {


    public class ContactRequest_BO
    {

        public bool SendContactRequest(string FirstName, string LastName, string Email, string Subject, string MsgContent)
        {
            bool Result = false;
            MailMessage objMessage = null;
            StringBuilder objEmailBody = new StringBuilder();
            string ToEmailAddress = null;
            string User = null;
            string Password = null;



            try
            {

                ToEmailAddress = ConfigurationManager.AppSettings.Get("Smtp_Server_To_Email");

                User = ConfigurationManager.AppSettings.Get("Smtp_Server_To_User");

                Password = ConfigurationManager.AppSettings.Get("Smtp_Server_To_Password");

                objMessage = new MailMessage(Email, ToEmailAddress );

                objMessage.Subject = Subject;

                objMessage.IsBodyHtml = true;

                objEmailBody.AppendLine("Message From: " + FirstName + " " + LastName);

                objEmailBody.AppendLine();

                objEmailBody.AppendLine(MsgContent);

                objMessage.Body = objEmailBody.ToString();



                SmtpClient objSmtpClient = new SmtpClient();

                objSmtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Smtp_Server_Port"));

                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                objSmtpClient.UseDefaultCredentials = false;

                objSmtpClient.Credentials = new System.Net.NetworkCredential( User, Password );

                objSmtpClient.Host = ConfigurationManager.AppSettings.Get("Smtp_Server_Host");

                objSmtpClient.Send(objMessage);

                Result = true;


            }
            catch (System.Exception objException)
            {
                throw new Exception("ContactRequest_BO::SendContactRequest Error: " + objException.Message);
            }


            return Result;


        }
    }

}

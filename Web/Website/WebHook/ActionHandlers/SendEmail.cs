using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Roomie.WebService;
using Roomie.WebService.DataStructures;
using Roomie.WebService.Managers;

using WebCommunicator;

using System.Text;

namespace Roomie.WebService.WebHook.ActionHandlers
{
    internal class SendEmail : ActionHandler
    {
        public SendEmail()
        { }


        public override void ProcessMessage(ComputerRecord callingComputer, Message request, Message response)
        {
            if (!request.Values.ContainsKey("To"))
            {
                response.ErrorMessage = "To not set.";
                return;
            }
            if (!request.Values.ContainsKey("Subject"))
            {
                response.ErrorMessage = "Subject not set.";
                return;
            }
            if (!request.Values.ContainsKey("Body"))
            {
                response.ErrorMessage = "Body not set.";
                return;
            }

            string to = request.Values["To"];
            string subject = request.Values["Subject"];
            string body = request.Values["Body"];

            bool sent;

            try
            {
                sent = Roomie.WebService.Common.MailerBot.SendMessage(to, subject, body);
                if (sent)
                    response.Values.Add("Response", "mail sent.");
                else
                    response.ErrorMessage = "unknown error sending message to " + to;
            }
            catch(System.Security.SecurityException)
            {
                response.ErrorMessage = "Server denied from sending mail.";
            }

        }
    }
}
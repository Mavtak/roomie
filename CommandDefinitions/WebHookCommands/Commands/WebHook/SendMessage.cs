using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Exceptions;
using WebCommunicator;

namespace Roomie.Desktop.Extensions.WebHookCommands.Commands.WebHook
{
    class SendMessage : RoomieCommand
    {
        public SendMessage()
            : base()
        {
            this.finalize();
        }

        //TODO: work with multiple engines
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var engine = context.Engine;
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var originalXml = context.OriginalXml;

            var webhookEngines = Common.GetWebHookEngines(engine);

            var outMessage = new Message();

            foreach (string variableName in scope.Names)
                outMessage.Values.Add(variableName, scope.GetValue(variableName));

            foreach (XmlNode node in originalXml.ChildNodes)
                outMessage.Payload.Add(node);

            Common.SendMessage(engine, outMessage);

        }
    }
}

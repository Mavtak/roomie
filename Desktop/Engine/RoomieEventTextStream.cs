using System;
using System.IO;
using System.Text;

namespace Roomie.Desktop.Engine
{
    //TODO: make disposible
    public class RoomieEventTextStream
    {
        private RoomieEngine engine;
        private TextWriter output;

        private TimeSpan timeStampInterval;
        private DateTime lastTimestamp;
        private RoomieThread lastThread;

        public RoomieEventTextStream(RoomieEngine engine, TextWriter output,  TimeSpan timeStampInterval)
        {
            this.engine = engine;
            this.output = output;
            this.timeStampInterval = timeStampInterval;

            this.lastTimestamp = DateTime.MinValue;
            this.lastThread = null;

            engine.ScriptMessageSent += new RoomieThreadEventHandler(engine_ScriptMessageSent);
        }

        void engine_ScriptMessageSent(object sender, RoomieThreadEventArgs eventArgs)
        {
            var builder = new StringBuilder();
            lock (this)
            {
                var now = DateTime.Now;
                var timeSinceLastTimestamp = now.Subtract(lastTimestamp);
                if (timeSinceLastTimestamp >= timeStampInterval)
                {
                    builder.AppendLine();
                    builder.AppendLine(now + ":");
                    lastTimestamp = now;
                }

                if (lastThread == null || lastThread != eventArgs.Thread)
                {
                    builder.AppendLine();//extra line break

                    builder.Append(eventArgs.Thread.Name);
                    builder.Append(":");
                    builder.Append(Environment.NewLine);

                    builder.Append("--------------------");
                    builder.AppendLine();

                }
                builder.Append(eventArgs.Message);
                builder.AppendLine();

                lastThread = eventArgs.Thread;
            }

            output.Write(builder.ToString());
            output.Flush();
        }
    }
}

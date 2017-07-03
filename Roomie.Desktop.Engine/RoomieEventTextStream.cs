using System;
using System.IO;
using System.Text;

namespace Roomie.Desktop.Engine
{
    //TODO: make disposible
    public class RoomieEventTextStream
    {
        private readonly TextWriter output;

        private readonly TimeSpan timeStampInterval;
        private DateTime _lastTimestamp;
        private RoomieThread _lastThread;

        public RoomieEventTextStream(RoomieEngine engine, TextWriter output,  TimeSpan timeStampInterval)
        {
            this.output = output;
            this.timeStampInterval = timeStampInterval;

            _lastTimestamp = DateTime.MinValue;
            _lastThread = null;

            engine.ScriptMessageSent += new RoomieThreadEventHandler(engine_ScriptMessageSent);
        }

        void engine_ScriptMessageSent(object sender, RoomieThreadEventArgs eventArgs)
        {
            var builder = new StringBuilder();
            lock (this)
            {
                var now = DateTime.Now;
                var timeSinceLastTimestamp = now.Subtract(_lastTimestamp);
                if (timeSinceLastTimestamp >= timeStampInterval)
                {
                    builder.AppendLine();
                    builder.AppendLine(now + ":");
                    _lastTimestamp = now;
                }

                if (_lastThread == null || _lastThread != eventArgs.Thread)
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

                _lastThread = eventArgs.Thread;
            }

            output.Write(builder.ToString());
            output.Flush();
        }
    }
}

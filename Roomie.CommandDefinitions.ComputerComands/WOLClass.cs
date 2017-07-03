using System.Net.Sockets;

namespace Roomie.CommandDefinitions.ComputerCommands
{
    /// <summary>
    /// class taken from http://www.codeproject.com/KB/IP/cswol.aspx
    /// </summary>
    internal class WOLClass : UdpClient
    {
        public WOLClass()
            : base()
        { }
        //this is needed to send broadcast packet

        public void SetClientToBrodcastMode()
        {
            if (this.Active)
                this.Client.SetSocketOption(SocketOptionLevel.Socket,
                                          SocketOptionName.Broadcast, 0);
        }
    }
}

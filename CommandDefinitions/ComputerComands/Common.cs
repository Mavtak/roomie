using System;
using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Roomie.Common.Exceptions;

namespace Roomie.CommandDefinitions.ComputerCommands
{
    public static class Common
    {
        /// <summary>
        /// MAC_ADDRESS should  look like '013FA049'
        /// method taken from http://www.codeproject.com/KB/IP/cswol.aspx
        /// </summary>
        /// <param name="MAC_ADDRESS"></param>
        internal static void WakeFunction(string MAC_ADDRESS)
        {
            WOLClass client = new WOLClass();
            client.Connect(new
               IPAddress(0xffffffff),  //255.255.255.255  i.e broadcast

               0x2fff); // port=12287 let's use this one 

            client.SetClientToBrodcastMode();
            //set sending bites

            int counter = 0;
            //buffer to be send

            byte[] bytes = new byte[1024];   // more than enough :-)

            //first 6 bytes should be 0xFF

            for (int y = 0; y < 6; y++)
                bytes[counter++] = 0xFF;
            //now repeate MAC 16 times

            for (int y = 0; y < 16; y++)
            {
                int i = 0;
                for (int z = 0; z < 6; z++)
                {
                    bytes[counter++] =
                        byte.Parse(MAC_ADDRESS.Substring(i, 2),
                        NumberStyles.HexNumber);
                    i += 2;
                }
            }

            //now send wake up packet

            int reterned_value = client.Send(bytes, 1024);
        }

        internal static void SuspendComputer(PowerState state, bool force)
        {
            string action;
            switch (state)
            {
                case PowerState.Suspend:
                    action = "sleep";
                    break;
                case PowerState.Hibernate:
                    action = "hybernate";
                    break;
                default:
                    throw new RoomieRuntimeException("Unknown power state \"" + state + "\"");
            }

            try
            {
                bool ret = System.Windows.Forms.Application.SetSuspendState(state, force, false);

                if (!ret)
                    throw new RoomieRuntimeException("Could not " + action + " computer.");
            }
            catch (Exception exception)
            {
                throw new RoomieRuntimeException("Could not " + action + " computer. " + exception.Message, exception);
            }
        }


        [DllImport("user32.dll")]
        private static extern void LockWorkStation();

        internal static void LockComputer()
        {
            try
            {
                LockWorkStation();
            }
            catch (Exception exception)
            {
                throw new RoomieRuntimeException("Could not lock coputer: " + exception.Message);
            }
        }

        public static Version LibraryVersion
        {
            get
            {
                return InternalLibraryVersion.GetLibraryVersion();
            }
        }
    }
}

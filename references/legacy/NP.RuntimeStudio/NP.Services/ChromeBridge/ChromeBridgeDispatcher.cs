using NP.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NP.Services.ChromeBridge
{
    public sealed class ChromeBridgeDispatcher
    {
        private static readonly ChromeBridgeDispatcher _current =
            new ChromeBridgeDispatcher();

        public static ChromeBridgeDispatcher Current
        {
            get { return _current; }
        }

        private ChromeBridgeDispatcher()
        {

        }

        //--------------------------------

        public void Dispatch(MessagePacket packet)
        {
            switch (packet.Action)
            {
                case BridgeAction.InsertCode:

                    InsertCode(packet);

                    break;

                case BridgeAction.SendFile:

                    SendFile(packet);

                    break;
            }
        }

        //--------------------------------

        private void InsertCode(MessagePacket packet)
        {
            MessageBox.Show(packet.payload.Content);
        }

        //--------------------------------

        private void SendFile(MessagePacket packet)
        {
            MessageBox.Show(
                packet.payload.FileName);

            MessageBox.Show(
                packet.payload.Content);
        }

    }
}

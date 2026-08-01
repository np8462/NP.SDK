using NP.SDK.Contracts.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NP.SDK.Contracts.Clients
{
    public interface IRuntimeClientTransport
    {
        void Connect();

        void Disconnect();

        void Send(
            RuntimeMessage message);
    }
}

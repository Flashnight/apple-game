using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryGame.Events
{
    public class StartMultiplayerEvent
    {
        public bool IsServer { get; private set; }

        public StartMultiplayerEvent(bool isServer)
        {
            IsServer = isServer;
        }
    }
}

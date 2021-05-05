using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_game.Connection.Messages
{
    public class UnexpectedDisconnectionEventArgs : EventArgs
    {
       public long connectionID;
        public UnexpectedDisconnectionEventArgs(long id)
        {
            connectionID = id;
        }
    }
}

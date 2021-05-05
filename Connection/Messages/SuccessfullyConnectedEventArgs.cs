using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_game.Connection.Messages
{
    public class SuccessfullyConnectedEventArgs : ConnectionBaseEventArgs
    {
        public SuccessfullyConnectedEventArgs(long id, string address) : base(id,address)
        {
        }
    }
}

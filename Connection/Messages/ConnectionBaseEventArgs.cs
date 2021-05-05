using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_game.Connection.Messages
{
    public class ConnectionBaseEventArgs : EventArgs
    {
        public long ConnectionID { get; }
        public string Address { get; }
        public ConnectionBaseEventArgs(long id_pol, string _adres)
        {
            ConnectionID = id_pol;
            Address = _adres;
        }
    }
}

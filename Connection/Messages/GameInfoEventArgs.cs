using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_game.Connection.Messages
{
    public class GameInfoEventArgs : EventArgs
    {
        public GameInfo initGameInfo;
        public long connectionId;
    }
}
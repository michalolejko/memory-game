using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_game.Connection.Messages
{
    [Serializable]
    public class GameInfo
    {
        public ConnectionEnums.gameDifficulty gameDifficulty;
        public ConnectionEnums.gameType gameType;
        public Deck deck;
    }
}

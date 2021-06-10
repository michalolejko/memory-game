using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_game.Connection
{
    public static class ConnectionEnums
    {
        public enum GameDifficulty
        {
            Easy,
            Medium,
            Hard,
            Custom
        }

        public enum GameType
        {
            Normal,
            ForTime //?
        }
        /* //unused
        public enum TypeOfMessage
        {
            NextTurn
        }*/

        public enum ResponseEnum
        {
            MyTurn,
            NotMyTurn,
            InitInfoReceived
        }
    }
}

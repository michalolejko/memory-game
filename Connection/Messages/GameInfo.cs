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
        public ConnectionEnums.GameDifficulty GameDifficulty { get; set; }
        public ConnectionEnums.GameType GameType { get; set; }
        public Deck Deck;
        public Card[] Cards { get; set; }
        public int currentPlayerConnectId;
        public bool gameInProgress;

        public GameInfo(Deck deck)
        {
            gameInProgress = false;
            this.Deck = deck;
            InitAndFillCardsArray();
            RandomizeArrangementOfCards();
        }

        private void RandomizeArrangementOfCards()
        {
            Random rnd = new Random();
            this.Cards = this.Cards.OrderBy(x => rnd.Next()).ToArray(); 
        }

        private void InitAndFillCardsArray()
        {
            int numberOfRows = Deck.cards.Count(), j = 0;
            this.Cards = new Card[2 * numberOfRows];
            for (int i = 0; i < numberOfRows; i++)
                Cards[i] = Deck.cards[i];
            for (int i = numberOfRows; i < 2 * numberOfRows; i++)
                Cards[i] = Deck.cards[j++];
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
            {
                GameInfo tmp = (GameInfo)obj;
                return (tmp.gameInProgress == this.gameInProgress && tmp.Deck.name==this.Deck.name);
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_game.Forms
{
    /* //////////////////////////////////////////////////////////////////////////////////////////////
     * 06.05
     * DO ROZWIAZANIA NA POTEM:
     * JAK ZROBIC WYSWIETLANIE KART NA SEKUNDE BEZ PROBLEMU DOSTEPU DO JEDNYCH ZASOBOW PRZY ASYNC?
    */ //////////////////////////////////////////////////////////////////////////////////////////////

    static class CardFunctions
    {
        public static int rowId1, colId1, rowId2, colId2, idCard1, idCard2;
        public static System.Windows.Forms.DataGridView cardsGridView;
        public static Connection.Messages.GameInfo gameInfo;

        /* public CardFunctions(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2, System.Windows.Forms.DataGridView cardsGridView, Connection.Messages.GameInfo gameInfo)
         {
             this.rowId1 = rowId1;
             this.rowId2 = rowId2;
             this.colId1 = colId1;
             this.colId2 = colId2;
             this.idCard1 = idCard1;
             this.idCard2 = idCard2;
             this.cardsGridView = cardsGridView;
             this.gameInfo = gameInfo;
         }*/

        /*
        public void ShowSelectedCardsForAWhile(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2, int time)
        {

            ShowSelectedCards(rowId1, colId1, rowId2, colId2, idCard1, idCard2);
            System.Threading.Thread.Sleep(time);
            HideSelectedCards(rowId1, colId1, rowId2, colId2, idCard1, idCard2);

        }
        public void ShowSelectedCards(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2)
        {
            this.cardsGridView.Rows[rowId1].Cells[colId1].Value = (Image)new Bitmap(gameInfo.Deck.cards[idCard1].Image, new Size(150, 150));
            this.cardsGridView.Rows[rowId2].Cells[colId2].Value = (Image)new Bitmap(gameInfo.Deck.cards[idCard2].Image, new Size(150, 150));
        }

        public void HideSelectedCards(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2)
        {
            Image blankImage = Image.FromFile(@"../../Resources/Cards/Blank.png");
            this.cardsGridView.Rows[rowId1].Cells[colId1].Value = blankImage;
            this.cardsGridView.Rows[rowId2].Cells[colId2].Value = blankImage;
        }
        */
        public static void UpdateInfo(int _rowId1, int _colId1, int _rowId2, int _colId2, int _idCard1, int _idCard2, System.Windows.Forms.DataGridView _cardsGridView, Connection.Messages.GameInfo _gameInfo)
        {
            rowId1 = _rowId1;
            rowId2 = _rowId2;
            colId1 = _colId1;
            colId2 = _colId2;
            idCard1 = _idCard1;
            idCard2 = _idCard2;
            cardsGridView = _cardsGridView;
            gameInfo = _gameInfo;
        }

        public static async Task ShowSelectedCardsForAWhile(int time)
        {

            await ShowSelectedCardsAsync();
            System.Threading.Thread.Sleep(time);
            HideSelectedCards();
        }
        public static void ShowSelectedCards()
        {
            cardsGridView.Rows[rowId1].Cells[colId1].Value = (Image)new Bitmap(gameInfo.Deck.cards[idCard1].Image, new Size(150, 150));
            cardsGridView.Rows[rowId2].Cells[colId2].Value = (Image)new Bitmap(gameInfo.Deck.cards[idCard2].Image, new Size(150, 150));
        }

        public static void HideSelectedCards()
        {
            Image blankImage = Image.FromFile(@"../../Resources/Cards/Blank.png");
            cardsGridView.Rows[rowId1].Cells[colId1].Value = blankImage;
            cardsGridView.Rows[rowId2].Cells[colId2].Value = blankImage;
        }
        private static Task ShowSelectedCardsAsync()
        {
            Task task = Task.Factory.StartNew(ShowSelectedCards);
            return task;
        }

    }
}

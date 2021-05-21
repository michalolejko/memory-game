using memory_game.Connection;
using System.Windows.Forms;
using memory_game;
using memory_game.Connection.Messages;
using memory_game.Forms;
using System;
using System.Drawing;

namespace Memory
{
    public partial class GameWindowClientForm : GameWindowForm
    {
        public GameWindowClientForm(Connect connection) : base(connection)
        {
            //InitializeComponent();
            base.EndMyTurn();
            cardsGridView.ClearSelection();
            if (TryDownloadGameInfo())
                UpdateCardGridBox();
        }


        //Pobrac info z serwera w razie dolaczenia do istniejacej gry
        private bool TryDownloadGameInfo()
        {
            return false;
        }

        private void GameWindowClientForm_Load(object sender, EventArgs e)
        {

        }
        protected override void BadChoice(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2)
        {
            //nie trafilem - oznaczam swoje ID jako ujemne
            gameInfo.currentPlayerConnectId = -gameInfo.currentPlayerConnectId;
            base.BadChoice(rowId1, colId1, rowId2, colId2, idCard1, idCard2);
        }
        public override void Con_GameInfoReceived(object sender, GameInfoEventArgs e)
        {
            base.Con_GameInfoReceived(sender, e);
            Console.WriteLine("Odebrano z id: " + e.connectionId + ", odebrano: " + gameInfo.currentPlayerConnectId);
            //0 = inicjalizacja gry
            if (gameInfo.currentPlayerConnectId == 0)
            {
                InitPopulateCellsByGameInfo();
                PopulateCardGridBoxWithBlankImages();
                FormFunctions.AppendColoredTextWithTime(richTextBox1, "Gra rozpoczęta", Color.Green);
                //connection.SendMessageToServer(e.gameInfo);
            }
            //jesli ujemna to nie moj ruch - aktualizuj (wywolywane z base) + sprawdz czy to nie jest koniec
            else if (gameInfo.currentPlayerConnectId < 0)
            {
                if (CheckIsItEndOfGame())
                    EndGame();
            }
            //jesli dodatnia - moj ruch 
            else if (gameInfo.currentPlayerConnectId > 0)
            {
                // bo ID = 1 to ruch serwera
                if (gameInfo.currentPlayerConnectId > 1)
                    StartMyTurn();
            }

            //0 = inicjalizacja gry
            /*if (gameInfo.currentPlayerConnectId == 0)
            {
                InitPopulateCellsByGameInfo();
                PopulateCardGridBoxWithBlankImages();
                FormFunctions.AppendColoredTextWithTime(richTextBox1, "Gra rozpoczęta", Color.Green);
                //connection.SendMessageToServer(e.gameInfo);
            }
            else
            {
                    if (CheckIsItEndOfGame())
                        EndGame();
                    //if(gameInfo.currentPlayerConnectId)
                    else if(gameInfo.currentPlayerConnectId>1)
                            StartMyTurn();
            }*/
        }

        protected override void EndMyTurn()
        {
            base.EndMyTurn();
            if (gameInfo is null)
                gameInfo = connection.gameInfo;
            if (gameInfo.gameInProgress)
                connection.SendMessageToServer(gameInfo);

            /*else
                connection.SendGameInfoToAllClients();*/
            /*if (gameInfo.gameInProgress)
                connection.SendMessageToServer(gameInfo);
            else
                EndGame();*/
            //connection.SendGameInfoToAllClients(gameInfo);
        }

    }
}

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
            if(TryDownloadGameInfo())
                UpdateCardGridBox();
        }


        //Pobrac info z serwera w razie dolaczenia do istniejacej gry
        private bool TryDownloadGameInfo()
        {
            return true; 
        }

        private void GameWindowClientForm_Load(object sender, EventArgs e)
        {

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
                connection.SendMessageToServer(e.gameInfo);
            }
            else
            {
                    UpdateCardGridBox();
                    if (CheckIsItEndOfGame())
                        EndGame();
                    else
                        StartMyTurn();
            }
        }

        protected override void EndMyTurn()
        {
            base.EndMyTurn();
            if (gameInfo is null)
                gameInfo = connection.gameInfo;


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

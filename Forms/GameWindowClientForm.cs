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
           
        }

        private void GameWindowClientForm_Load(object sender, EventArgs e)
        {

        }

        public override void Con_GameInfoReceived(object sender, GameInfoEventArgs e)
        {
            gameInfo = e.gameInfo;
            Console.WriteLine("Odebrano z id: " + e.connectionId + ", odebrano: " + gameInfo.currentPlayerConnectId);
            //0 = inicjalizacja gry
            if (gameInfo.currentPlayerConnectId == 0)
            {
                InitPopulateCellsByGameInfo();
                PopulateCardGridBoxWithBlankImages();
                FormFunctions.AppendColoredTextWithTime(richTextBox1, "Gra rozpoczęta", Color.Green);
            }
            else
            {
                //UpdateCardGridBox();
                StartMyTurn();
            }
        }

        protected override void EndMyTurn()
        {
            base.EndMyTurn();
            if (!(gameInfo is null))
                connection.SendGameInfoToAllClients(gameInfo);
        }
    }
}

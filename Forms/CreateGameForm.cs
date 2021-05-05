using memory_game;
using memory_game.Connection;
using memory_game.Connection.Messages;
using memory_game.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Memory
{
    public partial class CreateGameForm : Form
    {
        protected Connect connection;
        public CreateGameForm(/*bool isServerCreatorParam*/)
        {
            connection = new Connect();
            InitializeComponent();
        }

        protected virtual void startGameButton_Click(object sender, EventArgs e) { }
        protected void CreateGameWindow_FormClosed(object sender, FormClosedEventArgs e) {
            connection.fullDisconnect();
        }
        protected void backButton_Click(object sender, EventArgs e){
            connection.fullDisconnect();
            FormManager.NewWindow(this, new CreateOrConnectToServerForm());
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            /* GameInfo kom = new GameInfo();
             //kom.cardsState = l;
             kom.gameType = "other";
             con.wyslij(kom);*/
        }

        protected void con_UnexpectedDisctonnection(object sender, UnexpectedDisconnectionEventArgs e)
        {
            FormFunctions.AppendColoredText(infoTextBox, "Połączenie klienta o id: " + e.connectionID + " zerwane" + "\n", Color.Red);
            //klientOdlaczAsync(this); //wołana metoda bezpiecznej zmiany na formatce (ponieważ ten wątek nie jest właścicielem formatki).
        }

        protected void con_SuccessfullyConnected(object sender, SuccessfullyConnectedEventArgs e)
        {
            FormFunctions.AppendColoredText(infoTextBox, "Połączono z: ", Color.Red);
            FormFunctions.AppendColoredText(infoTextBox, e.Address.ToString() + "\n", Color.Blue);
        }

        protected void con_InitInfoReceived(object sender, GameInfoEventArgs e)
        {
            FormFunctions.AppendColoredText(infoTextBox, e.initGameInfo.gameType.ToString(), Color.Green);
            FormFunctions.AppendColoredText(infoTextBox, "\n", Color.Green);
        }

        protected void BlockStartGame(string message)
        {
            if (message != null)
            {
                tooltipLabel.Visible = true;
                tooltipLabel.Text = message;
            }
            startGameButton.Enabled = false;
        }

        protected void UnlockStartGame(string message)
        {
            if(message!=null)
                tooltipLabel.Text = message;
            startGameButton.Enabled = true;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
        }
    }
}

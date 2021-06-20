using memory_game;
using memory_game.Connection;
using memory_game.Connection.Messages;
using memory_game.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;
using static memory_game.Connection.ConnectionEnums;

namespace Memory
{
    public partial class CreateGameClientForm : CreateGameForm
    {
        public CreateGameClientForm() : base()
        {
            InitializeComponent();
            connectToServerButton.Select();
        }

        private void disconnectClientAsync(Form fm)
        {
            if (fm.InvokeRequired)
                fm.Invoke(new MethodInvoker(() => { connection.FullDisconnect(); }));
            else
                connection.FullDisconnect();
        }

        protected override void startGameButton_Click(object sender, EventArgs e)
        {
            FormManager.NewWindow(this, new GameWindowClientForm(connection));
        }

        private void connectToServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.StartClient(addressTextBox.Text, Int32.Parse(portTextBox.Text)))
                {
                    startGameButton.Visible = true;
                    successfulConnectedLabel.Visible = true;
                    /*
                    connection.GameInfoReceived += new Connect.GameInfoReceivedEventsHandler(pol_KomunikatPrzybyl);
                    connection.successfullyConnected += new Connect.PolaczenieUstanowioneEventsHandler(pol_PolaczenieUstanowione);
                    connection.unexpectedDisconnection += new Connect.UnexpectedDisconnectionEventsHandler(disconnectClientAsync);*/
                    connectToServerButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                FormFunctions.AppendColoredText(infoTextBox, "Błąd podłaczenia: \n", Color.Red);
                FormFunctions.AppendColoredText(infoTextBox, ex.Message + "\n", Color.Red);
            }
            startGameButton.Select();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}

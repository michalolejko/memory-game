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
        }

        private void disconnectClientAsync(Form fm)
        {
            if (fm.InvokeRequired)
                fm.Invoke(new MethodInvoker(() => { connection.fullDisconnect(); }));
            else
                connection.fullDisconnect();
        }

        protected override void startGameButton_Click(object sender, EventArgs e)
        {
            FormManager.NewWindow(this, new GameWindowClientForm(connection));
        }

        private void connectToServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.startClient(addressTextBox.Text, Int32.Parse(portTextBox.Text)))
                {
                    startGameButton.Visible = true;
                    successfulConnectedLabel.Visible = true;

                    //connection.initGameInfoReceived += new Connect.InitGameInfoReceivedEventsHandler(pol_KomunikatPrzybyl);
                    //connection.successfullyConnected += new Connect.PolaczenieUstanowioneEventsHandler(pol_PolaczenieUstanowione);
                    //connection.unexpectedDisconnection += new Connect.PolaczenieZerwaneEventsHandler(pol_PolaczenieKlientZerwane);
                }
            }
            catch (Exception ex)
            {
                FormFunctions.AppendColoredText(infoTextBox, "Błąd podłaczenia: \n", Color.Red);
                FormFunctions.AppendColoredText(infoTextBox, ex.Message + "\n", Color.Red);
            }

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}

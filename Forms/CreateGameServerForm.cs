using memory_game;
using memory_game.Connection;
using memory_game.Connection.Messages;
using memory_game.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static memory_game.Connection.ConnectionEnums;

namespace Memory
{
    public partial class CreateGameServerForm : CreateGameForm
    {
        private Deck deck;
        public CreateGameServerForm() : base()
        {
            BlockStartGame("Wybierz parametry gry");
            InitializeComponent();
            this.deck = new Deck();
            PopulateDecksListBox();
            InitWindowChecks();
        }
        private void InitWindowChecks()
        {
            if (!mediumDiffLvlButton.Checked && !hardDiffLvlButton.Checked && !customDiffLvlButton.Checked)
                easyDiffLvlButton.Checked = true;
            if (!gameModeTimeButton.Checked)
                gameModeNormalButton.Checked = true;
            if (decksListBox.Items.Count > 0)
                decksListBox.SetSelected(0, true);
            CheckIfGameCanBeStarted();
        }

        protected override void startGameButton_Click(object sender, EventArgs e)
        {
            GameInfo initGameInfo = new GameInfo
            {
                gameType = GetGameType(),
                gameDifficulty = GetgameDifficulty(),
                deck = this.deck
            };
            //Game.StartGame(initGameInfo, this);//zastanowic sie jak z tym game - czy raczej server i client uzywac
            FormManager.NewWindow(this, new GameWindowServerForm(connection));
        }

        private void uploadOwnDeckButton_Click(object sender, EventArgs e)
        {
            deck = new Deck();
            deck.UploadNewDeck();
            PopulateDecksListBox();
        }

        private gameType GetGameType()
        {
            if (gameModeNormalButton.Checked)
                return gameType.Normal;
            else
                return gameType.ForTime;
        }

        private gameDifficulty GetgameDifficulty()
        {
            if (easyDiffLvlButton.Checked)
                return gameDifficulty.Easy;
            else if (mediumDiffLvlButton.Checked)
                return gameDifficulty.Medium;
            else if (hardDiffLvlButton.Checked)
                return gameDifficulty.Hard;
            else return gameDifficulty.Custom;
        }

        private void createServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.startServer(addressTextBox.Text, Int32.Parse(portTextBox.Text)))
                {
                    connection.initGameInfoReceived += new Connect.InitGameInfoReceivedEventsHandler(con_InitInfoReceived);
                    connection.successfullyConnected += new Connect.SuccessfullyConnectedEventsHandler(con_SuccessfullyConnected);
                    connection.unexpectedDisconnection += new Connect.UnexpectedDisconnectionEventsHandler(con_UnexpectedDisctonnection);

                    tooltipLabel.Visible = true;
                    startGameButton.Visible = true;
                    CheckIfGameCanBeStarted();
                    tooltipLabel.Text += "\nPomyślnie stworzono serwer";
                }
            }
            catch (Exception ex)
            {
                FormFunctions.AppendColoredText(infoTextBox, "Bład połączenia: \n", Color.Red);
                FormFunctions.AppendColoredText(infoTextBox, ex.Message + "\n", Color.Red);
                tooltipLabel.Text = "Nie udało się stworzyć serwera";
            }

        }

        private void CreateGameServerForm_Load(object sender, EventArgs e)
        {

        }

        private void decksListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.deck = deck.FindDeckByName(decksListBox.SelectedItem.ToString());
            CheckIfGameCanBeStarted();
        }

        private void Any_SelectedIndexChanged(Object sender, EventArgs e)
        {
            CheckIfGameCanBeStarted();
        }

        private void PopulateDecksListBox()
        {
            Deck[] decks = this.deck.LoadDecks();
            List<string> decksList = new List<string>();
            foreach (Deck d in decks)
                decksList.Add(d.name);
            decksListBox.DataSource = decksList;
            CheckIfGameCanBeStarted();
        }

        private bool IsDeckLoaded()
        {
            //if isnt loaded
            if (this.deck == null || this.deck.name == "") 
            {
                this.deck = new Deck();
                return false;
            }
            return true;
        }

        private void CheckIfGameCanBeStarted()
        {
            if (!IsDeckLoaded())
                BlockStartGame("Wybierz talie");
            else if (!startGameButton.Visible)
                BlockStartGame("Wybierz parametry i stwórz serwer");
            else if (!(decksListBox.Items.Count > 0))
                BlockStartGame("Wgraj talie");
            else
                UnlockStartGame("Możesz rozpocząć grę");
        }
    }
}

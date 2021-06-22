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
        private GameInfo gameInfo;
        public CreateGameServerForm() : base()
        {
            BlockStartGame("Wybierz parametry gry");
            InitializeComponent();
            createServerButton.Select();
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
            //Game.StartGame(initGameInfo, this);//zastanowic sie jak z tym game - czy raczej server i client uzywac
            GameWindowServerForm gameWindowServer;

            if (easyDiffLvlButton.Checked)
                gameWindowServer = new GameWindowServerForm(connection, gameInfo, GameDifficulty.Easy);
            else if (mediumDiffLvlButton.Checked)
                gameWindowServer = new GameWindowServerForm(connection, gameInfo, GameDifficulty.Medium);
            else if (hardDiffLvlButton.Checked)
                gameWindowServer = new GameWindowServerForm(connection, gameInfo, GameDifficulty.Hard);
            else
            {
                gameWindowServer = new GameWindowServerForm(connection, gameInfo, GameDifficulty.Custom);
                gameWindowServer.SetCustomDifficultyTime(Int32.Parse(customDiffTimeTextBox.Text));
            }

            FormManager.NewWindow(this, gameWindowServer);
        }

        private GameInfo TakeInitGameInfo()
        {
            return new GameInfo(this.deck)
            {
                GameType = GetGameType(),
                GameDifficulty = GetgameDifficulty(),
            };
        }

        private void uploadOwnDeckButton_Click(object sender, EventArgs e)
        {
            Deck localVariable = new Deck();
            localVariable.UploadNewDeck();
            PopulateDecksListBox();
        }

        private GameType GetGameType()
        {
            if (gameModeNormalButton.Checked)
                return GameType.Normal;
            else
                return GameType.ForTime;
        }

        private GameDifficulty GetgameDifficulty()
        {
            if (easyDiffLvlButton.Checked)
                return GameDifficulty.Easy;
            else if (mediumDiffLvlButton.Checked)
                return GameDifficulty.Medium;
            else if (hardDiffLvlButton.Checked)
                return GameDifficulty.Hard;
            else return GameDifficulty.Custom;
        }

        private void createServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.gameInfo = TakeInitGameInfo();
                if (connection.StartServer(addressTextBox.Text, Int32.Parse(portTextBox.Text), this.gameInfo))
                {
                    connection.GameInfoReceived += new Connect.GameInfoReceivedEventsHandler(con_GameInfoReceived);
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
            startGameButton.Select();
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
            if (customDiffLvlButton.Checked)
            {
                customDiffTimeTextBox.Visible = true;
                customDiffTimeLabel.Visible = true;
            }
            else
            {
                customDiffTimeTextBox.Visible = false;
                customDiffTimeLabel.Visible = false;
            }

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

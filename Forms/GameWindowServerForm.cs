using memory_game.Connection;
using System.Windows.Forms;
using memory_game;
using memory_game.Connection.Messages;
using memory_game.Forms;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace Memory
{
    public partial class GameWindowServerForm : GameWindowForm
    {
        private Task responseTask;
        private CancellationTokenSource responseSource;
        public GameWindowServerForm(Connect connection, GameInfo gameInfo, ConnectionEnums.GameDifficulty gameDifficulty) : base(connection)
        {
            Console.WriteLine("____________________SERWER____________________\n");
            //con.KomunikatPrzybyl += new Connect.KomunikatEventsHandler(pol_KomunikatPrzybyl);
            /* connection.GameInfoReceived += new Connect.GameInfoReceivedEventsHandler(Con_GameInfoReceived);
             connection.successfullyConnected += new Connect.SuccessfullyConnectedEventsHandler(Con_SuccessfullyConnected);
             connection.unexpectedDisconnection += new Connect.UnexpectedDisconnectionEventsHandler(Con_UnexpectedDisctonnection);*/
            this.gameDifficulty = gameDifficulty;
            InitializeComponent();
            this.gameInfo = gameInfo;
            InitPopulateCellsByGameInfo();
            PopulateCardGridBoxWithBlankImages();
            EndMyTurn();
        }

        public override void Con_SuccessfullyConnected(object sender, SuccessfullyConnectedEventArgs e)
        {
        }

        public override void Con_GameInfoReceived(object sender, GameInfoEventArgs e)
        {
            Console.WriteLine("Serwer odebral wartosc " + e.gameInfo.currentPlayerConnectId);
            //jesli jest zerem to serwer otrzymal response
            if (e.gameInfo.currentPlayerConnectId == 0)
            {
                Console.WriteLine("Serwer otrzymal response");
                //klient otrzymal info, ze teraz jego tura
                if (e.gameInfo.ResponseEnum == ConnectionEnums.ResponseEnum.MyTurn)
                {
                    responseSource.Cancel();
                    Console.WriteLine("Klient o id " + e.gameInfo.myId + " otrzymał info, o swojej turze");
                }
                //potwierdzenie, ze klient otrzymal init info
                if (e.gameInfo.ResponseEnum == ConnectionEnums.ResponseEnum.InitInfoReceived)
                {
                    Console.WriteLine("Otrzymalem init response od id = " + e.gameInfo.myId);
                    if (e.gameInfo.cells != gameInfo.cells)
                    {
                        Console.WriteLine("Wysylam ponownie informacje do klienta o id: " + e.gameInfo.myId);
                        connection.SendGameInfoToPlayerById(connection.gameInfo, (int)e.gameInfo.myId);
                    }
                }
            }
            else
            {
                base.Con_GameInfoReceived(sender, e);
                if (CheckIsItEndOfGame())
                    EndGame();
                //jesli ID ujemne - klient nie trafil, wyslij do reszty klientow i zarzadzaj nastepna ture
                else if (gameInfo.currentPlayerConnectId < 0)
                {
                    connection.SendGameInfoExceptOne(gameInfo, -gameInfo.currentPlayerConnectId);
                    ShowSelectedCardsForAWhile(gameInfo, displayTimeOfCards);
                    gameInfo.currentPlayerConnectId = -gameInfo.currentPlayerConnectId;
                    //jesli nastepna tura zwraca 1 to znaczy, ze teraz tura serwera
                    int nextTurnId = connection.NextTurn(gameInfo);
                    if (gameInfo.gameInProgress && nextTurnId == 1)
                    {
                        Console.WriteLine("-");
                        StartMyTurn();
                    }
                    else
                    {
                        Console.WriteLine("Tworze responseTask oczekujacy na potwierdzenie tury klienta o id " + nextTurnId);
                        int counter = 1;
                        // TryItAgain:
                        responseSource = new CancellationTokenSource();
                        responseTask = Task.Run(async delegate
                        {
                            await Task.Delay(2000, responseSource.Token);
                        });
                        try
                        {
                            responseTask.Wait();
                            if (!responseTask.IsCanceled && counter < 10)
                            {
                                counter++;
                                Console.WriteLine("Klient nie odebral info o swojej turze, probuje " + counter + " raz");
                                connection.SendGameInfoToPlayerById(gameInfo, nextTurnId);
                                //goto TryItAgain;
                            }
                            if (responseTask.IsCanceled || responseSource.IsCancellationRequested)
                            {
                                responseSource = null;
                                responseTask = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("responseTask exception msg: " + ex.Message);
                            responseSource = null;
                            responseTask = null;
                        }
                    }
                }
                //jesli ID dodatnie - trafil, poinformuj reszte klientow 
                else if (gameInfo.currentPlayerConnectId > 0)
                {
                    Console.WriteLine("Klient o id '" + gameInfo.myId + "' trafil");
                    gameInfo.currentPlayerConnectId = -gameInfo.currentPlayerConnectId;
                    connection.SendGameInfoToAllClients();
                }
            }
            /*
            //if(gameInfo.currentPlayerConnectId > connection.clientsList.Count)
            if (gameInfo.currentPlayerConnectId == 0)
            {

                // return;
            }
            if (gameInfo.gameInProgress && connection.NextTurn(gameInfo) == 1)
                StartMyTurn();
            else
                connection.SendGameInfoToAllClients(e.gameInfo);
            UpdateCardGridBox();*/
        }
        protected override void GoodChoice(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2)
        {
            gameInfo.currentPlayerConnectId = 1;
            base.GoodChoice(rowId1, colId1, rowId2, colId2, idCard1, idCard2);

        }
        private void GameWindowServerForm_Load(object sender, EventArgs e)
        {

        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            if (gameDifficulty != ConnectionEnums.GameDifficulty.Custom)
                switch (gameDifficulty)
                {
                    case ConnectionEnums.GameDifficulty.Hard:
                        {
                            displayTimeOfCards = 500;
                            break;
                        }
                    case ConnectionEnums.GameDifficulty.Medium:
                        {
                            displayTimeOfCards = 1000;
                            break;
                        }
                    case ConnectionEnums.GameDifficulty.Easy:
                        {
                            displayTimeOfCards = 1500;
                            break;
                        }
                }
            else
                gameInfo.colId1 = displayTimeOfCards;
            gameInfo.gameInProgress = true;
            gameInfo.GameDifficulty = this.gameDifficulty;
            gameInfo.ResponseEnum = ConnectionEnums.ResponseEnum.InitInfoSent;
            int nextTurnId = connection.TryStartGameAsServer(gameInfo);
            if (nextTurnId == 1)
                StartMyTurn();
            myIdInfo.Text = "Moje ID: 1";
            FillPlayersList(connection.GetNumberOfClients());
            playerListBox.SetSelected(--nextTurnId, true);
            this.startGameButton.Text = "Rozpoczęto";
            this.startGameButton.Enabled = false;
            FormFunctions.AppendColoredTextWithTime(richTextBox1, "Gra rozpoczęta", Color.Green);
        }

        protected override void EndMyTurn()
        {
            base.EndMyTurn();
            if (gameInfo.gameInProgress && connection.NextTurn(gameInfo) == 1)
                StartMyTurn();
        }

        protected override void BadChoice(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2)
        {
            base.BadChoice(rowId1, colId1, rowId2, colId2, idCard1, idCard2);
            if (gameInfo.currentPlayerConnectId > 0)
                gameInfo.currentPlayerConnectId = -gameInfo.currentPlayerConnectId;
            else if (gameInfo.currentPlayerConnectId == 0)
                gameInfo.currentPlayerConnectId = -1;
            connection.SendGameInfoToAllClients(gameInfo);
        }
        /*protected override void StartMyTurn()
        {
            base.StartMyTurn();

        }*/
        public void SetCustomDifficultyTime(int cstmTime)
        {
            displayTimeOfCards = cstmTime;
        }
    }
}
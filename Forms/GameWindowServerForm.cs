using memory_game.Connection;
using System.Windows.Forms;
using memory_game;
using memory_game.Connection.Messages;
using memory_game.Forms;
using System;
using System.Drawing;
using System.Threading;

namespace Memory
{
    public partial class GameWindowServerForm : GameWindowForm
    {
        public GameWindowServerForm(Connect connection, GameInfo gameInfo) : base(connection)
        {
            //con.KomunikatPrzybyl += new Connect.KomunikatEventsHandler(pol_KomunikatPrzybyl);
            /* connection.GameInfoReceived += new Connect.GameInfoReceivedEventsHandler(Con_GameInfoReceived);
             connection.successfullyConnected += new Connect.SuccessfullyConnectedEventsHandler(Con_SuccessfullyConnected);
             connection.unexpectedDisconnection += new Connect.UnexpectedDisconnectionEventsHandler(Con_UnexpectedDisctonnection);*/
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
                //1.
                //teraz przydaloby sie zliczyc ilosc responsow - jesli jest mniejsza niz ilosc 
                //graczy w kolejce to znaczy, ze ktos nie otrzymal wiadomosci:
                //1.1
                //w connect jest responseCounter - przy nextTurn jest zerowany,
                //response powinien go zwiekszac, a jesli != liczbie graczy to cos nie tak i rozeslij jeszcze raz
                //1.2
                //W ConnectionEnums dalem ResponseEnum (statycznie) - mozna dac np - moja tura, nie moja tura
                //2. 
                //mozna porownac wiadomosc z responsa do swojej np przy starcie gry:
                //jesli tablica cells sie nie zgadza na poczatku to wyslij jeszcze raz init info
                //mozna np jesli cos sie nie przeslalo (jakos to sprawdzic-ze cos jest nullem np) to jeszcze raz

                //1.2
                Console.WriteLine("Serwer otrzymal response");
                if(e.gameInfo.ResponseEnum == ConnectionEnums.ResponseEnum.InitInfoReceived)
                {
                    Console.WriteLine("Otrzymalem init response od id = " + e.gameInfo.myId);
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
                    connection.SendGameInfoToAllClients(gameInfo);
                    ShowSelectedCardsForAWhile(gameInfo, 1000);
                    gameInfo.currentPlayerConnectId = -gameInfo.currentPlayerConnectId;
                    //jesli nastepna tura zwraca 1 to znaczy, ze teraz tura serwera
                    if (gameInfo.gameInProgress && connection.NextTurn(gameInfo) == 1)
                        StartMyTurn();
                }
                //jesli ID dodatnie - trafil, poinformuj reszte klientow 
                else if (gameInfo.currentPlayerConnectId > 0)
                {
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
            gameInfo.gameInProgress = true;
            if (connection.TryStartGameAsServer(gameInfo) == 1)
                StartMyTurn();
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
            // connection.SendGameInfoToAllClients(gameInfo);
            base.BadChoice(rowId1, colId1, rowId2, colId2, idCard1, idCard2);
        }
        /*protected override void StartMyTurn()
        {
            base.StartMyTurn();

        }*/
    }
}

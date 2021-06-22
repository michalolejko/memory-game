using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using static System.Windows.Forms.ListBox;
using System.IO;
using memory_game.Connection;
using memory_game.Forms;
using memory_game.Connection.Messages;
using System.Threading;

namespace Memory
{
    public partial class GameWindowForm : Form
    {
        protected System.Threading.Thread cardGrdiBoxThread;
        protected Connect connection;
        protected GameInfo gameInfo;
        protected int[,] cells;
        protected int numberOfCards, numberOfColumns, numberOfRows, cardsInLastRow;
        protected long myId;
        private int myScore;
        private bool isEndOfGame;
        private Image blankImage;
        private List<string> playersList;
        protected ConnectionEnums.GameDifficulty gameDifficulty;
        protected int displayTimeOfCards;

        public int[,] Cells
        {
            get
            {
                if (cells is null)
                    Cells = gameInfo.cells;
                return cells;
            }
            set { cells = value; }
        }

        public GameWindowForm(Connect connection)
        {
            playersList = new List<string>();
            myId = -256;
            gameInfo = new GameInfo();
            gameInfo.gameInProgress = false;
            blankImage = Image.FromFile(@"../../Resources/Cards/Blank.png");
            isEndOfGame = false;
            myScore = 0;
            InitializeComponent();
            this.connection = connection;
            FormFunctions.AppendColoredTextWithTime(richTextBox1, "Połączono", Color.Green);
            connection.GameInfoReceived += new Connect.GameInfoReceivedEventsHandler(Con_GameInfoReceived);
            connection.successfullyConnected += new Connect.SuccessfullyConnectedEventsHandler(Con_SuccessfullyConnected);
            connection.unexpectedDisconnection += new Connect.UnexpectedDisconnectionEventsHandler(Con_UnexpectedDisctonnection);

        }

        public virtual void Con_UnexpectedDisctonnection(object sender, UnexpectedDisconnectionEventArgs e)
        {
        }

        public virtual void Con_SuccessfullyConnected(object sender, SuccessfullyConnectedEventArgs e)
        {
        }

        public virtual void Con_GameInfoReceived(object sender, GameInfoEventArgs e)
        {
            try
            {
                Console.WriteLine("Otrzymalem id = " + e.gameInfo.currentPlayerConnectId);
                int itemToSelect = e.gameInfo.currentPlayerConnectId;
                if (itemToSelect == 0)
                    itemToSelect = e.gameInfo.rowId2;
                else if (itemToSelect < 0)
                    itemToSelect = -itemToSelect;
                Console.WriteLine("Do zaznaczenia na liscie graczy: " + itemToSelect);
                /*for (int i = 0; i <= playerListBox.Items.Count-1; i++)
                    if(itemToSelect != i)
                        playerListBox.SetSelected(i, false);
                    else*/
                playerListBox.SetSelected(--itemToSelect, true);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception przy oznaczaniu gracza: " + exc.Message);
            }
            if (cardsGridView.Rows.Count < numberOfRows)
                new Thread(new ThreadStart(DebugLastRowInGridBoxWithBlankImages));
            if (cardGrdiBoxThread != null && cardGrdiBoxThread.IsAlive)
                cardGrdiBoxThread.Join();
            if (e.gameInfo.ResponseEnum == ConnectionEnums.ResponseEnum.InitInfoSent && myId < -200)
            {
                myId = e.gameInfo.myId;
                myIdInfo.Text = "Moje ID: " + myId.ToString();
                FillPlayersList(e.gameInfo.rowId1);
            }
            if (isEndOfGame)
                return;
            if (!e.gameInfo.gameInProgress)
                EndGame();
            gameInfo = e.gameInfo;
            UpdateCardGridBox();
            DebugCellsArray();
            System.Threading.Thread.Sleep(5);
            
            //if (e.connectionId > 0)
            //    FormFunctions.AppendColoredTextWithTime(richTextBox1, e.connectionId.ToString() + " - utrata ruchu", Color.Orange);

                /*Console.WriteLine("czy to moje ID?: " + connection.IsItMyId(e.connectionId));
                Console.WriteLine("czy 1 to moje ID?: " + connection.IsItMyId(1L));
                Console.WriteLine("czy 2 to moje ID?: " + connection.IsItMyId(2L));*/
                //Console.WriteLine("Z klasy connect: " + connection.getConnectId());
        }

        protected void FillPlayersList(int maxId)
        {
            if (playerListBox.Items.Count > 0)
                return;
            for(int i = 1; i <= maxId+1; i++)
            {
                playersList.Add("Player " + i);
                playerListBox.Items.Add("Player " + i);
            }
        }
        protected void Form2_Load(object sender, EventArgs e)
        {
            this.cardsGridView.ClearSelection();
            /*
            listView1.View = View.Details;
            listView1.Columns.Add("test", 150);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            */
        }
        protected virtual void EndMyTurn()
        {
            BlockSelectionInCardsGridView();
        }
        private void BlockSelectionInCardsGridView()
        {
            this.cardsGridView.Enabled = false;
            this.cardsGridView.ClearSelection();
        }
        protected virtual void StartMyTurn()
        {
            this.cardsGridView.ClearSelection();
            if (CheckIsItEndOfGame())
                EndGame();
            else
            {
                this.cardsGridView.Enabled = true;
                tooltipLabel.Text = "Ruch: Twój ruch";
                FormFunctions.AppendColoredTextWithTime(richTextBox1, "Twój ruch", Color.Green);
            }
        }
        private void CalculateNumberOfCardColumnsRowsAndLastRow()
        {
            numberOfCards = gameInfo.Cards.Count();
            numberOfColumns = (int)Math.Ceiling(Math.Sqrt(numberOfCards));//zaokraglanie w gore pierwiastka kwadratowego z liczby ilosc kart
            numberOfRows = (int)Math.Ceiling((double)numberOfCards / (double)numberOfColumns);
            cardsInLastRow = numberOfColumns * numberOfRows - numberOfCards;
            if (cardsInLastRow <= 0)
                cardsInLastRow = numberOfColumns;
        }
        protected void InitPopulateCellsByGameInfo()
        {
            if (gameInfo == null) return;
            if (numberOfColumns < 1 && numberOfRows < 1)
                CalculateNumberOfCardColumnsRowsAndLastRow();
            cells = new int[numberOfRows, numberOfColumns];
            for (int i = 0; i < numberOfRows; i++)
            {
                int numberOfColumnsInThisRow = i == numberOfRows - 1 ? cardsInLastRow : numberOfColumns;
                for (int j = 0; j < numberOfColumnsInThisRow; j++)
                    cells[i, j] = gameInfo.Cards[(i * numberOfColumns) + j].Id;
                if (numberOfColumnsInThisRow != numberOfColumns)
                    for (int j = numberOfColumnsInThisRow; j < numberOfColumns; j++)
                        cells[i, j] = -1;
            }
            Console.WriteLine("Zainicializowalem cells z gameInfo");
        }

        protected void PopulateCardGridBoxWithBlankImages()
        {
            if (gameInfo == null) return;
            Console.WriteLine("PopulateCardGridBoxWithBlankImages: gameInfo nie jest nullem");
            if (cells == null) InitPopulateCellsByGameInfo();
            Console.WriteLine("PopulateCardGridBoxWithBlankImages: tworze kolumny");
            for (int i = 0; i < numberOfColumns; i++)
            {
                DataGridViewImageColumn column = new DataGridViewImageColumn();
                column.ImageLayout = DataGridViewImageCellLayout.Stretch;
                cardsGridView.Columns.Add(column);
            }
            Console.WriteLine("PopulateCardGridBoxWithBlankImages: tworze wiersze");
            for (int i = 0; i < numberOfRows; i++)
            {
                //cardsGridView.Rows.Add();
                Image[] imagesInRow = new Image[numberOfColumns];
                for (int j = 0; j < numberOfColumns; j++)
                    if (cells[i, j] >= 0)
                        imagesInRow[j] = blankImage;
                Console.WriteLine("Iteracja wierszy: " + i + ", Dodaje: " + imagesInRow.Length + " obrazkow");
                try
                {
                    Console.WriteLine(cardsGridView.Rows.Add(imagesInRow));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception when adding rows to card grid view: " + ex.Message);
                }
            }
            Console.WriteLine("Wypełniłem grid box pustymi obrazkami");
            if (cardGrdiBoxThread != null && cardGrdiBoxThread.IsAlive)
                cardGrdiBoxThread.Abort();
        }
        protected void DebugLastRowInGridBoxWithBlankImages()
        {
            //cardsGridView.Rows.Add();
            if (cardGrdiBoxThread != null && cardGrdiBoxThread.IsAlive)
                cardGrdiBoxThread.Join();
            Image[] imagesInRow = new Image[numberOfColumns];
            for (int j = 0; j < numberOfColumns; j++)
                if (cells[numberOfRows, j] >= 0)
                    imagesInRow[j] = blankImage;
            Console.WriteLine("Iteracja wierszy: " + numberOfRows + ", Dodaje: " + imagesInRow.Length + " obrazkow");
            try
            {
                Console.WriteLine(cardsGridView.Rows.Add(imagesInRow));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception when adding rows to card grid view: " + ex.Message);
            }
            if (cardGrdiBoxThread != null && cardGrdiBoxThread.IsAlive)
                cardGrdiBoxThread.Abort();
        }
        protected void UpdateCardGridBox()
        {
            if (gameInfo == null)
            {
                Console.WriteLine("Odebrano gameInfo jako null!");
                return;
            }
            if (numberOfColumns < 1 || numberOfRows < 1)
            {
                Console.WriteLine("Inicjalizuje gridBox, poniewaz ilosc kolumn wynosi {0}, lub ilosc wierszy wynosi {0}", numberOfColumns, numberOfRows);
                InitPopulateCellsByGameInfo();
            }
            if (!(gameInfo.cells is null))
            {
                Console.WriteLine("Aktualizuje tablice cells");
                cells = gameInfo.cells;
            }
            for (int i = 0; i < numberOfRows; i++)
                for (int j = 0; j < numberOfColumns; j++)
                    if (cells[i, j] >= 100)
                    {
                        Console.WriteLine("Cells: ");
                        foreach (int cell in cells)
                            Console.Write(cell + " ");
                        Console.WriteLine("\n" + cells[i, j] + " - Zdekodowano cell: " + DecodeHittedCard(i, j));
                        ShowSelectedCard(i, j, DecodeHittedCard(i, j));
                    }


            //Console.WriteLine("Columns: " + numberOfColumns + " Rows: " + numberOfRows);
        }
        private int DecodeHittedCard(int row, int col)
        {
            if (cells[row, col] < 100)
                return -1;
            int tmp = cells[row, col] / 100;
            return --tmp;
        }

        private int DecodeHittedCard(int cell)
        {
            if (cell < 100)
                return -1;
            int tmp = cell / 100;
            return --tmp;
        }
        private void ShowSelectedCard(int rowId, int colId, int idCard)
        {
            this.cardsGridView.Rows[rowId].Cells[colId].Value = (Image)new Bitmap(gameInfo.Deck.cards[idCard].Image, new Size(150, 150));
        }
        protected bool CheckIsItEndOfGame()
        {
            foreach (int cell in cells)
                if (cell < 100)
                    return false;
            return true;
        }

        protected virtual void EndGame()
        {
            if (isEndOfGame)
                return;
            gameInfo.currentPlayerConnectId = -1;
            gameInfo.cells = cells;
            isEndOfGame = true;
            gameInfo.gameInProgress = false;
            connection.SendGameInfoToAllClients(gameInfo);
            tooltipLabel.Text = "Koniec gry";
            FormFunctions.AppendColoredTextWithTime(richTextBox1, tooltipLabel.Text, Color.Green);
            BlockSelectionInCardsGridView();
            ShowAllCards();
        }

        protected void DebugCardGridBox()
        {
            //powoduje bledy, a nie je naprawia aktualnie
            /*
            if (cells.Length > numberOfColumns + numberOfRows || gameInfo.cells.Length > numberOfColumns+numberOfRows)
            {
                Console.WriteLine("Debugowanie CardGridBox'a:\ncells.Length: " + cells.Length + ", gi.cells.length: " + gameInfo.cells.Length + ", col+row: " + numberOfRows + numberOfColumns);
                InitPopulateCellsByGameInfo();
                UpdateCardGridBox();
            }*/
        }

        protected void DebugCellsArray()
        {
            //1. Jesli jedna komorka jest zaznaczona, a druga o takim samym kodzie nie to rowniez ja zaznacza
            int[,] tmp = cells;
            foreach (int localCell in tmp)
                if (localCell > 100)
                    for (int i = 0; i < numberOfRows; i++)
                        for (int j = 0; j < numberOfColumns; j++)
                            if (CoddedValueOfCell(cells[i, j]) == localCell)
                                CodeHitInCells(i, j);
            //-------------------------------------------------------------------
            //2. Przypisuje kod odkrytych obrazkow // blad - klient nie wyswietla tablicy przy starcie, mimo ze jest if (?)
            /* if (gameInfo.gameInProgress)
                 for (int i = 0; i < numberOfRows; i++)
                     for (int j = 0; j < numberOfColumns; j++)
                         if (cells[i, j] < 100 &&
                             (Image)this.cardsGridView.Rows[i].Cells[j].Value != blankImage)
                             CodeHitInCells(i, j);
             */
            //odswieza obrazki
            UpdateCardGridBox();
        }

        private void ShowAllCards()
        {
            //index out of bound
            Console.WriteLine("Pokazuje wszystkie karty, ilosc rzedow: " + numberOfRows + ", ilosc kolumn: " + numberOfColumns);
            for (int i = 0; i < numberOfRows; i++)
                for (int j = 0; j < numberOfColumns; j++)
                {
                    if (cells[i, j] < 100)
                        CodeHitInCells(i, j);
                    ShowSelectedCard(i, j, DecodeHittedCard(cells[i, j]));
                }
        }

        private bool IsCardAvailableOnThisCell(int rowId1, int colId1, int rowId2, int colId2)
        {
            if (cells[rowId1, colId1] < 0
                || cells[rowId2, colId2] < 0
                || cells[rowId1, colId1] >= 100
                || cells[rowId2, colId2] >= 100)
                return false;
            return true;
        }

        protected void ShowSelectedCardsForAWhile(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2, int time)
        {
            //Sleep nie dziala, stworzono klase CardFunctions dla zarzadzania watkami i Async, ale jest do poprawy (06.05)
            /*
            ShowSelectedCards(rowId1, colId1, rowId2, colId2, idCard1, idCard2);
            System.Threading.Thread.Sleep(time);
            HideSelectedCards(rowId1, colId1, rowId2, colId2, idCard1, idCard2);
            */
            Console.WriteLine("Pokaz na chwile karte...");
            CardFunctions.UpdateInfo(rowId1, colId1, rowId2, colId2, idCard1, idCard2, cardsGridView, gameInfo);
            /*Task task */
            _ = CardFunctions.ShowSelectedCardsForAWhile(displayTimeOfCards);
            //task.Wait();
        }

        protected void ShowSelectedCardsForAWhile(GameInfo gi, int time)
        {
            Console.WriteLine("Pokaz karte o parametrach: \nr1: {0}, r2: {0}, c1: {0}, c2: {0}, id1: {0}, id2: {0}", gi.rowId1, gi.rowId2, gi.colId1, gi.colId2, gi.idCard1, gi.idCard2);
            CardFunctions.UpdateInfo(gi.rowId1, gi.colId1, gi.rowId2, gi.colId2, gi.idCard1, gi.idCard2, cardsGridView, gi);
            _ = CardFunctions.ShowSelectedCardsForAWhile(displayTimeOfCards);
        }
        private void ShowSelectedCards(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2)
        {
            this.cardsGridView.Rows[rowId1].Cells[colId1].Value = (Image)new Bitmap(gameInfo.Deck.cards[idCard1].Image, new Size(150, 150));
            this.cardsGridView.Rows[rowId2].Cells[colId2].Value = (Image)new Bitmap(gameInfo.Deck.cards[idCard2].Image, new Size(150, 150));
        }

        private void HideSelectedCards(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2)
        {
            Image blankImage = Image.FromFile(@"../../Resources/Cards/Blank.png");
            this.cardsGridView.Rows[rowId1].Cells[colId1].Value = blankImage;
            this.cardsGridView.Rows[rowId2].Cells[colId2].Value = blankImage;
        }

        protected virtual void GoodChoice(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2)
        {
            AddPoints();
            ShowSelectedCards(rowId1, colId1, rowId2, colId2, idCard1, idCard2);
            tooltipLabel.Text = "Ruch: Trafiono parę! Grasz dalej.";
            FormFunctions.AppendColoredTextWithTime(richTextBox1, tooltipLabel.Text, Color.Green);
            CodeHitInCells(rowId1, colId1);
            CodeHitInCells(rowId2, colId2);
            gameInfo.cells = cells;
            connection.SendGameInfoToAllClients(gameInfo);
            if (CheckIsItEndOfGame())
                EndGame();
            //connection.SendGameInfoToAllClients(gameInfo);
            /*Console.WriteLine("Stan tablicy cells po trafieniu: ");
            foreach (int cell in cells)
                Console.WriteLine(cell + ", ");*/
        }

        private void CodeHitInCells(int row, int col)
        {
            if (cells[row, col] >= 100)
                return;
            cells[row, col]++;
            cells[row, col] *= 100;
        }

        private int CoddedValueOfCell(int cell)
        {
            if (cell >= 100)
                return cell;
            cell++;
            return cell *= 100;
        }

        private bool IsItHittedCell(int row, int col)
        {
            return cells[row, col] >= 100;
        }

        private bool IsItHittedCell(int cell)
        {
            return cell >= 100;
        }

        protected virtual void BadChoice(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2)
        {
            tooltipLabel.Text = "Ruch: Nie trafiłeś! Tracisz turę.";
            FormFunctions.AppendColoredTextWithTime(richTextBox1, tooltipLabel.Text, Color.Red);
            ShowSelectedCardsForAWhile(rowId1, colId1, rowId2, colId2, idCard1, idCard2, displayTimeOfCards);
            gameInfo.rowId1 = rowId1;
            gameInfo.rowId2 = rowId2;
            gameInfo.colId1 = colId1;
            gameInfo.colId2 = colId2;
            gameInfo.idCard1 = idCard1;
            gameInfo.idCard2 = idCard2;

            EndMyTurn();
        }
        /*
        //--------------------------------------------------------------------------------------
        private void UpdateCardsArrayInGameInfo()
        {
            for(int i = 0;i < numberOfRows; i++)
                for (int j = 0; j < numberOfColumns; j++)
                {
                    if(cells[i,j]<100)
                        this.gameInfo.Cards[i + (j * numberOfRows)] = this.gameInfo.Deck.cards[cells[i, j]];
                    //else
                }
            //i+(j*numberOfRows)
        }
        */
        private void CheckCards(int rowId1, int colId1, int rowId2, int colId2)
        {
            int idCard1 = cells[rowId1, colId1], idCard2 = cells[rowId2, colId2];
            if (idCard1 == idCard2)
                GoodChoice(rowId1, colId1, rowId2, colId2, idCard1, idCard2);
            else
                BadChoice(rowId1, colId1, rowId2, colId2, idCard1, idCard2);

        }
        private void AddPoints()
        {
            scoreLabel.Text = (++myScore).ToString();
        }
        private void AddPoints(int pointsToAdd)
        {
            scoreLabel.Text = (pointsToAdd + myScore).ToString();
        }

        protected void cardsGridView_SelectionChanged(object sender, EventArgs e)
        {
            //max 2 cells selected
            if (this.cardsGridView.SelectedCells.Count >= 2)
            {
                int colId1 = this.cardsGridView.SelectedCells[0].ColumnIndex, colId2 = this.cardsGridView.SelectedCells[1].ColumnIndex;
                int rowId1 = this.cardsGridView.SelectedCells[0].RowIndex, rowId2 = this.cardsGridView.SelectedCells[1].RowIndex;
                if (this.cardsGridView.SelectedCells != null)
                {
                    this.cardsGridView.SelectedCells[1].Selected = false;
                    this.cardsGridView.SelectedCells[0].Selected = false;
                }
                if (IsCardAvailableOnThisCell(rowId1, colId1, rowId2, colId2))
                    CheckCards(rowId1, colId1, rowId2, colId2);
                else
                    this.tooltipLabel.Text = "Tooltip: Spróbuj zaznaczyć inne karty, możesz użyć do tego CTRL";
            }

        }

        protected void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.FullDisconnect();
        }

        protected void disconnect_Click(object sender, EventArgs e)
        {
            connection.FullDisconnect();
            disconnectLabel.Visible = true;
            MessageBox.Show("Rozłączono. Gra skończona", "Koniec", MessageBoxButtons.OK);
            this.Close();
        }
        protected void confirmButton_Click(object sender, EventArgs e)
        {
            /*
            if (selected.Count == 2)
            {
                if (selected.Contains(1) && !selected.Contains(2))
                {
                    label4.Text = "Brawo, punkt dla Ciebie!";
                    points++;
                    label5.Text = "Wynik: " + points;
                }
                else
                {
                    label4.Text = "Niestety, zła odpowiedź";
                }
            }
            label4.Visible = true;
            */
        }

        private void autoScrollRtb1Button_Click(object sender, EventArgs e)
        {
            FormFunctions.autoScroll = FormFunctions.autoScroll ? false : true;
            this.autoScrollRtb1Button.Text = FormFunctions.autoScroll ? "Wyłacz autoscroll" : "Włącz autoscroll";
            if (FormFunctions.autoScroll)
                this.richTextBox1.ScrollToCaret();
        }


        protected void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //do usuniecia (zamiast listView1 jest cardGridView
        protected void listView1_Click(object sender, EventArgs e)
        {
            /*
            if (listView1.SelectedItems.Count > 1)
            {
                //var i1 = listView1.SelectedItems[0].SubItems[0].Text;
                //var i2 = listView1.SelectedItems[1].SubItems[0].Text;
                string m;
                string k;
                GameInfo kom = new GameInfo();
                ListViewItem temp = listView1.SelectedItems[0];
                ListViewItem temp2 = listView1.SelectedItems[1];
                int t1 = temp.ImageIndex;
                int t2 = temp2.ImageIndex;
                int k1 = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                int k2 = listView1.Items.IndexOf(listView1.SelectedItems[1]);
                //listView1.Items.Add(lvTemp.Items[t1]);
                //listView1.Items.Add(lvTemp.Items[t2]);
                pictureBox1.Image = imgs.Images[t1];
                pictureBox2.Image = imgs.Images[t2];
                if (t1 == t2)
                {
                    MessageBox.Show("Brawo! trafienie");
                    kom.matched = true;
                    m = "tak";
                    kom.gCard1 = k1;
                    kom.gCard2 = k2;
                    //kom.gIndex = t1;
                    listView1.Items.RemoveAt(k1);
                    listView1.Items.RemoveAt(k2 - 1);
                    points++;
                    label1.Text = points.ToString();
                    label1.Refresh();
                }
                else
                {
                    MessageBox.Show("błędne trafienie");
                    kom.matched = false;
                    m = "nie";
                }
                kom.gameType = "trafienie " + m;
                List<Image> il = new List<Image>();
                il.Add(Image.FromFile("D:/testimg/t1.jpeg"));
                kom.imgs = il;
                con.wyslij(kom);
            }*/
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

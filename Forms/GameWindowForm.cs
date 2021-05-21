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

namespace Memory
{
    public partial class GameWindowForm : Form
    {
        protected Connect connection;
        protected GameInfo gameInfo;
        protected int[,] cells;
        protected int numberOfCards, numberOfColumns, numberOfRows, cardsInLastRow;
        private int myScore;

        public GameWindowForm(Connect connection)
        {
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
            gameInfo = e.gameInfo;
            if(e.connectionId > 0)
                FormFunctions.AppendColoredTextWithTime(richTextBox1, e.connectionId.ToString() + " - utrata ruchu", Color.Orange);
            /*Console.WriteLine("czy to moje ID?: " + connection.IsItMyId(e.connectionId));
            Console.WriteLine("czy 1 to moje ID?: " + connection.IsItMyId(1L));
            Console.WriteLine("czy 2 to moje ID?: " + connection.IsItMyId(2L));*/
            //Console.WriteLine("Z klasy connect: " + connection.getConnectId());
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
        }


        protected void PopulateCardGridBoxWithBlankImages()
        {
            if (gameInfo == null) return;
            if (cells == null) InitPopulateCellsByGameInfo();
            Image blankImage = Image.FromFile(@"../../Resources/Cards/Blank.png");
            for (int i = 0; i < numberOfColumns; i++)
            {
                DataGridViewImageColumn column = new DataGridViewImageColumn();
                column.ImageLayout = DataGridViewImageCellLayout.Stretch;
                cardsGridView.Columns.Add(column);
            }
            for (int i = 0; i < numberOfRows; i++)
            {
                Image[] imagesInRow = new Image[numberOfColumns];
                for (int j = 0; j < numberOfColumns; j++)
                    if (cells[i, j] >= 0)
                        imagesInRow[j] = blankImage;
                cardsGridView.Rows.Add(imagesInRow);
            }
        }
        protected void UpdateCardGridBox()
        {
            if (gameInfo == null) return;
            if (numberOfColumns < 1 || numberOfRows < 1)
                InitPopulateCellsByGameInfo();
            for (int i = 0; i < numberOfRows; i++)
                for (int j = 0; j < numberOfColumns; j++)
                    if (cells[i, j] >= 100)
                    {
                        Console.WriteLine(DecodeHittedCard(i,j));
                        ShowSelectedCard(i, j, DecodeHittedCard(i, j));
                    }
                        

            //Console.WriteLine("Columns: " + numberOfColumns + " Rows: " + numberOfRows);
        }
        private int DecodeHittedCard(int row, int col)
        {
            if (cells[row, col] < 100)
                return -1;
            return cells[row,col]-- / 100;
        }
        private void ShowSelectedCard(int rowId, int colId, int idCard)
        {
            this.cardsGridView.Rows[rowId].Cells[colId].Value = (Image)new Bitmap(gameInfo.Deck.cards[idCard].Image, new Size(150, 150));
        }
        protected bool CheckIsItEndOfGame()
        {
            int counter = 0;
            //int cardsLeft = 0;
            foreach (int cell in cells)
                if (IsItHittedCell(cell))
                    counter++;
            /*else
                cardsLeft++;
        Console.WriteLine("Licznik trafień: "+counter/2+", pozostało jeszcze: "+cardsLeft/2);*/
            //Console.WriteLine("Trafiono: " + counter + ", ogolnie: " +cells.Length);
            return counter >= cells.Length;
        }

        protected virtual void EndGame()
        {
            tooltipLabel.Text = "Koniec gry";
            FormFunctions.AppendColoredTextWithTime(richTextBox1, tooltipLabel.Text, Color.Green);
            BlockSelectionInCardsGridView();
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

        private void ShowSelectedCardsForAWhile(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2, int time)
        {
            //Sleep nie dziala, stworzono klase CardFunctions dla zarzadzania watkami i Async, ale jest do poprawy (06.05)
            /*
            ShowSelectedCards(rowId1, colId1, rowId2, colId2, idCard1, idCard2);
            System.Threading.Thread.Sleep(time);
            HideSelectedCards(rowId1, colId1, rowId2, colId2, idCard1, idCard2);
            */

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

        private void GoodChoice(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2)
        {
            AddPoints();
            ShowSelectedCards(rowId1, colId1, rowId2, colId2, idCard1, idCard2);
            tooltipLabel.Text = "Ruch: Trafiono parę! Grasz dalej.";
            FormFunctions.AppendColoredTextWithTime(richTextBox1, tooltipLabel.Text, Color.Green);
            CodeHitInCells(rowId1, colId1);
            CodeHitInCells(rowId2, colId2);
            if (CheckIsItEndOfGame())
                EndGame();
            connection.SendGameInfoToAllClients(gameInfo);
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

        private bool IsItHittedCell(int row, int col)
        {
            return cells[row, col] >= 100;
        }

        private bool IsItHittedCell(int cell)
        {
            return cell >= 100;
        }

        private void BadChoice(int rowId1, int colId1, int rowId2, int colId2, int idCard1, int idCard2)
        {
            ShowSelectedCardsForAWhile(rowId1, colId1, rowId2, colId2, idCard1, idCard2, 1000);
            tooltipLabel.Text = "Ruch: Nie trafiłeś! Tracisz turę.";
            FormFunctions.AppendColoredTextWithTime(richTextBox1, tooltipLabel.Text, Color.Red);
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

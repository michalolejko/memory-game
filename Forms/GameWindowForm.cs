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

namespace Memory
{
    public partial class GameWindowForm : Form
    {
        protected Connect connection;
        public GameWindowForm(Connect connection)
        {
            InitializeComponent();
            this.connection = connection;
            FormFunctions.AppendColoredText(richTextBox1, "polaczono\n", Color.Green);
        }

        protected void Form2_Load(object sender, EventArgs e)
        {
            /*
            listView1.View = View.Details;
            listView1.Columns.Add("test", 150);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            */
        }

        protected void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.fullDisconnect();
        }

        protected void disconnect_Click(object sender, EventArgs e)
        {
            connection.fullDisconnect();
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

        protected void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        protected void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

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
    }
}

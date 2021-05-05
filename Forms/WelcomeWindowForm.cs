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
using memory_game;

namespace Memory
{
    public partial class WelcomeWindowForm : Form
    {
        public WelcomeWindowForm()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            FormManager.NewWindow(this, new CreateOrConnectToServerForm());
        }

        private void Statistics_Click(object sender, EventArgs e)
        {
            FormManager.NewWindow(this, new RankingForm());
        }

        private void ExitGame_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WelcomeWindowForm_Load(object sender, EventArgs e)
        {

        }

        private void label_Click(object sender, EventArgs e)
        {

        }
    }
}

using memory_game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory
{
    public partial class CreateOrConnectToServerForm : Form
    {
        public CreateOrConnectToServerForm()
        {
            InitializeComponent();
        }

        private void connectToServerButton_Click(object sender, EventArgs e)
        {
            FormManager.NewWindow(this, new CreateGameClientForm());
        }

        private void createServerButton_Click(object sender, EventArgs e)
        {
            FormManager.NewWindow(this, new CreateGameServerForm());
        }
           
        private void CreateOrConnectToServerForm_Load(object sender, EventArgs e)
        {

        }
    }
}

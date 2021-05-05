using memory_game.Connection;
using System.Windows.Forms;
using memory_game;
using memory_game.Connection.Messages;
using memory_game.Forms;
using System;
using System.Drawing;

namespace Memory
{
    public partial class GameWindowServerForm : GameWindowForm
    {
        public GameWindowServerForm(Connect connection) : base(connection)
        {
            //InitializeComponent();
        }

        private void GameWindowServerForm_Load(object sender, EventArgs e)
        {

        }
    }
}

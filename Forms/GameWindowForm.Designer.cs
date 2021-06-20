using System;

namespace Memory
{
    partial class GameWindowForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        protected System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.connectButton = new System.Windows.Forms.Button();
            this.connectLabel = new System.Windows.Forms.Label();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.disconnectLabel = new System.Windows.Forms.Label();
            this.confirmButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.scoreTextLabel = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.myIdInfo = new System.Windows.Forms.Label();
            this.tooltipLabel = new System.Windows.Forms.Label();
            this.cardsGridView = new System.Windows.Forms.DataGridView();
            this.autoScrollRtb1Button = new System.Windows.Forms.Button();
            this.playerListLabel = new System.Windows.Forms.Label();
            this.playerListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.cardsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(242, 523);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Połącz";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Visible = false;
            // 
            // connectLabel
            // 
            this.connectLabel.AutoSize = true;
            this.connectLabel.Location = new System.Drawing.Point(323, 533);
            this.connectLabel.Name = "connectLabel";
            this.connectLabel.Size = new System.Drawing.Size(108, 13);
            this.connectLabel.TabIndex = 4;
            this.connectLabel.Text = "Pomyślnie połączono";
            this.connectLabel.Visible = false;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(12, 528);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(75, 23);
            this.disconnectButton.TabIndex = 5;
            this.disconnectButton.Text = "Zakończ";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // disconnectLabel
            // 
            this.disconnectLabel.AutoSize = true;
            this.disconnectLabel.Location = new System.Drawing.Point(101, 533);
            this.disconnectLabel.Name = "disconnectLabel";
            this.disconnectLabel.Size = new System.Drawing.Size(65, 13);
            this.disconnectLabel.TabIndex = 6;
            this.disconnectLabel.Text = "Rozłączono";
            this.disconnectLabel.Visible = false;
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(823, 523);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(125, 23);
            this.confirmButton.TabIndex = 8;
            this.confirmButton.Text = "Zatwierdź wybór";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Visible = false;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(447, 533);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // scoreTextLabel
            // 
            this.scoreTextLabel.AutoSize = true;
            this.scoreTextLabel.Location = new System.Drawing.Point(12, 482);
            this.scoreTextLabel.Name = "scoreTextLabel";
            this.scoreTextLabel.Size = new System.Drawing.Size(43, 13);
            this.scoreTextLabel.TabIndex = 10;
            this.scoreTextLabel.Text = "Wynik: ";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(15, 36);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(100, 220);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 263);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 13;
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Location = new System.Drawing.Point(51, 482);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(13, 13);
            this.scoreLabel.TabIndex = 15;
            this.scoreLabel.Text = "0";
            // 
            // myIdInfo
            // 
            this.myIdInfo.AutoSize = true;
            this.myIdInfo.Location = new System.Drawing.Point(1060, 25);
            this.myIdInfo.Name = "myIdInfo";
            this.myIdInfo.Size = new System.Drawing.Size(134, 13);
            this.myIdInfo.TabIndex = 18;
            this.myIdInfo.Text = "Jeszcze nie przydzielono id";
            // 
            // tooltipLabel
            // 
            this.tooltipLabel.AutoSize = true;
            this.tooltipLabel.Location = new System.Drawing.Point(526, 533);
            this.tooltipLabel.Name = "tooltipLabel";
            this.tooltipLabel.Size = new System.Drawing.Size(261, 13);
            this.tooltipLabel.TabIndex = 19;
            this.tooltipLabel.Text = "Tooltip: Aby zaznaczyć dwa obrazki przytrzymaj CTRL";
            // 
            // cardsGridView
            // 
            this.cardsGridView.AllowUserToAddRows = false;
            this.cardsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cardsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.cardsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cardsGridView.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cardsGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.cardsGridView.Location = new System.Drawing.Point(133, 25);
            this.cardsGridView.Name = "cardsGridView";
            this.cardsGridView.RowHeadersVisible = false;
            this.cardsGridView.Size = new System.Drawing.Size(849, 481);
            this.cardsGridView.TabIndex = 20;
            this.cardsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.cardsGridView.SelectionChanged += new System.EventHandler(this.cardsGridView_SelectionChanged);
            // 
            // autoScrollRtb1Button
            // 
            this.autoScrollRtb1Button.Location = new System.Drawing.Point(15, 7);
            this.autoScrollRtb1Button.Name = "autoScrollRtb1Button";
            this.autoScrollRtb1Button.Size = new System.Drawing.Size(100, 23);
            this.autoScrollRtb1Button.TabIndex = 21;
            this.autoScrollRtb1Button.Text = "Wyłącz autoscroll";
            this.autoScrollRtb1Button.UseVisualStyleBackColor = true;
            this.autoScrollRtb1Button.Click += new System.EventHandler(this.autoScrollRtb1Button_Click);
            // 
            // playerListLabel
            // 
            this.playerListLabel.AutoSize = true;
            this.playerListLabel.Location = new System.Drawing.Point(1060, 80);
            this.playerListLabel.Name = "playerListLabel";
            this.playerListLabel.Size = new System.Drawing.Size(66, 13);
            this.playerListLabel.TabIndex = 22;
            this.playerListLabel.Text = "Lista graczy:";
            // 
            // playerList
            // 
            this.playerListBox.FormattingEnabled = true;
            this.playerListBox.Location = new System.Drawing.Point(1063, 96);
            this.playerListBox.Name = "playerList";
            this.playerListBox.Size = new System.Drawing.Size(120, 407);
            this.playerListBox.TabIndex = 23;
            this.playerListBox.Click += new System.EventHandler(this.playerListBox_Click);
            this.playerListBox.DoubleClick += new System.EventHandler(this.playerListBox_Click);            
            // 
            // GameWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 563);
            this.Controls.Add(this.playerListBox);
            this.Controls.Add(this.playerListLabel);
            this.Controls.Add(this.autoScrollRtb1Button);
            this.Controls.Add(this.cardsGridView);
            this.Controls.Add(this.tooltipLabel);
            this.Controls.Add(this.myIdInfo);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.scoreTextLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.disconnectLabel);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.connectLabel);
            this.Controls.Add(this.connectButton);
            this.Name = "GameWindowForm";
            this.Text = "Memory game";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cardsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void playerListBox_Click(object sender, EventArgs e)
        {
            //do nothing
        }

        #endregion

        protected System.Windows.Forms.Button connectButton;
        protected System.Windows.Forms.Label connectLabel;
        protected System.Windows.Forms.Button disconnectButton;
        protected System.Windows.Forms.Label disconnectLabel;
        protected System.Windows.Forms.Button confirmButton;
        protected System.Windows.Forms.Label label4;
        protected System.Windows.Forms.Label scoreTextLabel;
        protected System.Windows.Forms.RichTextBox richTextBox1;
        protected System.Windows.Forms.TextBox textBox1;
        protected System.Windows.Forms.Label scoreLabel;
        protected System.Windows.Forms.Label myIdInfo;
        protected System.Windows.Forms.Label tooltipLabel;
        protected System.Windows.Forms.DataGridView cardsGridView;
        protected System.Windows.Forms.Button autoScrollRtb1Button;
        private System.Windows.Forms.Label playerListLabel;
        protected System.Windows.Forms.ListBox playerListBox;
    }
}
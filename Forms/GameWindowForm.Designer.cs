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
            this.connectButton = new System.Windows.Forms.Button();
            this.connectLabel = new System.Windows.Forms.Label();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.disconnectLabel = new System.Windows.Forms.Label();
            this.confirmButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tooltipLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.connectButton.Location = new System.Drawing.Point(242, 523);
            this.connectButton.Name = "button1";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Połącz";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Visible = false;
           // this.connectButton.Click += new System.EventHandler(this.connect_Click);
            // 
            // label2
            // 
            this.connectLabel.AutoSize = true;
            this.connectLabel.Location = new System.Drawing.Point(323, 533);
            this.connectLabel.Name = "label2";
            this.connectLabel.Size = new System.Drawing.Size(108, 13);
            this.connectLabel.TabIndex = 4;
            this.connectLabel.Text = "Pomyślnie połączono";
            this.connectLabel.Visible = false;
            // 
            // button2
            // 
            this.disconnectButton.Location = new System.Drawing.Point(12, 528);
            this.disconnectButton.Name = "button2";
            this.disconnectButton.Size = new System.Drawing.Size(75, 23);
            this.disconnectButton.TabIndex = 5;
            this.disconnectButton.Text = "Zakończ";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // label3
            // 
            this.disconnectLabel.AutoSize = true;
            this.disconnectLabel.Location = new System.Drawing.Point(101, 533);
            this.disconnectLabel.Name = "label3";
            this.disconnectLabel.Size = new System.Drawing.Size(65, 13);
            this.disconnectLabel.TabIndex = 6;
            this.disconnectLabel.Text = "Rozłączono";
            this.disconnectLabel.Visible = false;
            // 
            // button4
            // 
            this.confirmButton.Location = new System.Drawing.Point(823, 523);
            this.confirmButton.Name = "button4";
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
            // label5
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Location = new System.Drawing.Point(12, 482);
            this.scoreLabel.Name = "label5";
            this.scoreLabel.Size = new System.Drawing.Size(43, 13);
            this.scoreLabel.TabIndex = 10;
            this.scoreLabel.Text = "Wynik: ";
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
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(133, 25);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(815, 481);
            this.listView1.TabIndex = 14;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 482);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1054, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 105);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(1054, 206);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(118, 109);
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1054, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Zaznaczone obrazki";
            // 
            // label7
            // 
            this.tooltipLabel.AutoSize = true;
            this.tooltipLabel.Location = new System.Drawing.Point(526, 533);
            this.tooltipLabel.Name = "label7";
            this.tooltipLabel.Size = new System.Drawing.Size(254, 13);
            this.tooltipLabel.TabIndex = 19;
            this.tooltipLabel.Text = "Tooltip: Aby zaznaczyć dwa obrazki przytrzymaj CTRL";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 563);
            this.Controls.Add(this.tooltipLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.disconnectLabel);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.connectLabel);
            this.Controls.Add(this.connectButton);
            this.Name = "Form2";
            this.Text = "Memory game";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button connectButton;
        protected System.Windows.Forms.Label connectLabel;
        protected System.Windows.Forms.Button disconnectButton;
        protected System.Windows.Forms.Label disconnectLabel;
        protected System.Windows.Forms.Button confirmButton;
        protected System.Windows.Forms.Label label4;
        protected System.Windows.Forms.Label scoreLabel;
        protected System.Windows.Forms.RichTextBox richTextBox1;
        protected System.Windows.Forms.TextBox textBox1;
        protected System.Windows.Forms.ListView listView1;
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.PictureBox pictureBox1;
        protected System.Windows.Forms.PictureBox pictureBox2;
        protected System.Windows.Forms.Label label6;
        protected System.Windows.Forms.Label tooltipLabel;
    }
}
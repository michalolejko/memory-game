namespace Memory
{
    partial class CreateGameForm
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
            this.backButton = new System.Windows.Forms.Button();
            this.startGameButton = new System.Windows.Forms.Button();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.addressLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.infoTextBox = new System.Windows.Forms.RichTextBox();
            this.testButton = new System.Windows.Forms.Button();
            this.tooltipLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.backButton.Location = new System.Drawing.Point(12, 415);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "Powrót";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // button2
            // 
            this.startGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.startGameButton.Location = new System.Drawing.Point(48, 246);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(203, 46);
            this.startGameButton.TabIndex = 7;
            this.startGameButton.Text = "Rozpocznij rozgrywkę";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Visible = false;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);         
            // 
            // textBox1
            // 
            this.portTextBox.Location = new System.Drawing.Point(48, 203);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(100, 20);
            this.portTextBox.TabIndex = 11;
            this.portTextBox.Text = "2222";
            // 
            // textBox2
            // 
            this.addressTextBox.Location = new System.Drawing.Point(48, 155);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(100, 20);
            this.addressTextBox.TabIndex = 12;
            this.addressTextBox.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(45, 139);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(74, 13);
            this.addressLabel.TabIndex = 13;
            this.addressLabel.Text = "Adres serwera";
            // 
            // label3
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(45, 187);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(26, 13);
            this.portLabel.TabIndex = 14;
            this.portLabel.Text = "Port";
            // 
            // richTextBox1
            // 
            this.infoTextBox.Location = new System.Drawing.Point(48, 26);
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.Size = new System.Drawing.Size(186, 94);
            this.infoTextBox.TabIndex = 16;
            this.infoTextBox.Text = "";
            // 
            // button6
            // 
            this.testButton.Location = new System.Drawing.Point(12, 375);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 17;
            this.testButton.Text = "test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // tooltipLabel
            // 
            this.tooltipLabel.AutoSize = true;
            this.tooltipLabel.Location = new System.Drawing.Point(45, 295);
            this.tooltipLabel.Name = "successfulCreatedServerLabel";
            this.tooltipLabel.Size = new System.Drawing.Size(139, 13);
            this.tooltipLabel.TabIndex = 10;
            this.tooltipLabel.Text = "Pomyślnie stworzono serwer";
            this.tooltipLabel.Visible = false;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tooltipLabel);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.infoTextBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.startGameButton);
            this.Controls.Add(this.backButton);
            this.Name = "CreateGameWindow";
            this.Text = "Memory game - creating server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateGameWindow_FormClosed);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.Controls.SetChildIndex(this.tooltipLabel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button backButton;
        protected System.Windows.Forms.Button startGameButton;
        protected System.Windows.Forms.Label tooltipLabel;
        protected System.Windows.Forms.TextBox portTextBox;
        protected System.Windows.Forms.TextBox addressTextBox;
        protected System.Windows.Forms.Label addressLabel;
        protected System.Windows.Forms.Label portLabel;
        protected System.Windows.Forms.RichTextBox infoTextBox;
        protected System.Windows.Forms.Button testButton;
    }
}
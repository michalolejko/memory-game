namespace Memory
{
    partial class CreateGameServerForm
    {
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.gameModeNormalButton = new System.Windows.Forms.RadioButton();
            this.gameModeTimeButton = new System.Windows.Forms.RadioButton();
            this.gameModeGroupBox = new System.Windows.Forms.GroupBox();
            this.difficultyLevelGroupBox = new System.Windows.Forms.GroupBox();
            this.customDiffLvlButton = new System.Windows.Forms.RadioButton();
            this.hardDiffLvlButton = new System.Windows.Forms.RadioButton();
            this.mediumDiffLvlButton = new System.Windows.Forms.RadioButton();
            this.easyDiffLvlButton = new System.Windows.Forms.RadioButton();
            this.decksListBox = new System.Windows.Forms.ListBox();
            this.uploadOwnDeckButton = new System.Windows.Forms.Button();
            this.createServerButton = new System.Windows.Forms.Button();
            
            this.gameModeGroupBox.SuspendLayout();
            this.difficultyLevelGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // gameModeNormalButton
            // 
            this.gameModeNormalButton.AutoSize = true;
            this.gameModeNormalButton.Location = new System.Drawing.Point(15, 35);
            this.gameModeNormalButton.Name = "gameModeNormalButton";
            this.gameModeNormalButton.Size = new System.Drawing.Size(61, 17);
            this.gameModeNormalButton.TabIndex = 1;
            this.gameModeNormalButton.TabStop = true;
            this.gameModeNormalButton.Text = "Zwykła";
            this.gameModeNormalButton.UseVisualStyleBackColor = true;
            this.gameModeNormalButton.CheckedChanged += new System.EventHandler(this.Any_SelectedIndexChanged);
            // 
            // gameModeTimeButton
            // 
            this.gameModeTimeButton.AutoSize = true;
            this.gameModeTimeButton.Location = new System.Drawing.Point(15, 69);
            this.gameModeTimeButton.Name = "gameModeTimeButton";
            this.gameModeTimeButton.Size = new System.Drawing.Size(64, 17);
            this.gameModeTimeButton.TabIndex = 2;
            this.gameModeTimeButton.TabStop = true;
            this.gameModeTimeButton.Text = "Na czas";
            this.gameModeTimeButton.UseVisualStyleBackColor = true;
            this.gameModeTimeButton.CheckedChanged += new System.EventHandler(this.Any_SelectedIndexChanged);
            // 
            // gameModeGroupBox
            // 
            this.gameModeGroupBox.Controls.Add(this.gameModeTimeButton);
            this.gameModeGroupBox.Controls.Add(this.gameModeNormalButton);
            this.gameModeGroupBox.Location = new System.Drawing.Point(292, 289);
            this.gameModeGroupBox.Name = "gameModeGroupBox";
            this.gameModeGroupBox.Size = new System.Drawing.Size(200, 118);
            this.gameModeGroupBox.TabIndex = 4;
            this.gameModeGroupBox.TabStop = false;
            this.gameModeGroupBox.Text = "Tryb rozgrywki";
            // 
            // difficultyLevelGroupBox
            // 
            this.difficultyLevelGroupBox.Controls.Add(this.customDiffLvlButton);
            this.difficultyLevelGroupBox.Controls.Add(this.hardDiffLvlButton);
            this.difficultyLevelGroupBox.Controls.Add(this.mediumDiffLvlButton);
            this.difficultyLevelGroupBox.Controls.Add(this.easyDiffLvlButton);
            this.difficultyLevelGroupBox.Location = new System.Drawing.Point(292, 12);
            this.difficultyLevelGroupBox.Name = "difficultyLevelGroupBox";
            this.difficultyLevelGroupBox.Size = new System.Drawing.Size(200, 244);
            this.difficultyLevelGroupBox.TabIndex = 5;
            this.difficultyLevelGroupBox.TabStop = false;
            this.difficultyLevelGroupBox.Text = "Poziom trudności";
            // 
            // customDiffLvlButton
            // 
            this.customDiffLvlButton.AutoSize = true;
            this.customDiffLvlButton.Location = new System.Drawing.Point(18, 168);
            this.customDiffLvlButton.Name = "customDiffLvlButton";
            this.customDiffLvlButton.Size = new System.Drawing.Size(86, 17);
            this.customDiffLvlButton.TabIndex = 6;
            this.customDiffLvlButton.TabStop = true;
            this.customDiffLvlButton.Text = "Użytkownika";
            this.customDiffLvlButton.UseVisualStyleBackColor = true;
            this.customDiffLvlButton.CheckedChanged += new System.EventHandler(this.Any_SelectedIndexChanged);
            // 
            // hardDiffLvlButton
            // 
            this.hardDiffLvlButton.AutoSize = true;
            this.hardDiffLvlButton.Location = new System.Drawing.Point(18, 124);
            this.hardDiffLvlButton.Name = "hardDiffLvlButton";
            this.hardDiffLvlButton.Size = new System.Drawing.Size(58, 17);
            this.hardDiffLvlButton.TabIndex = 6;
            this.hardDiffLvlButton.TabStop = true;
            this.hardDiffLvlButton.Text = "Trudny";
            this.hardDiffLvlButton.UseVisualStyleBackColor = true;
            this.hardDiffLvlButton.CheckedChanged += new System.EventHandler(this.Any_SelectedIndexChanged);
            // 
            // mediumDiffLvlButton
            // 
            this.mediumDiffLvlButton.AutoSize = true;
            this.mediumDiffLvlButton.Location = new System.Drawing.Point(18, 78);
            this.mediumDiffLvlButton.Name = "mediumDiffLvlButton";
            this.mediumDiffLvlButton.Size = new System.Drawing.Size(55, 17);
            this.mediumDiffLvlButton.TabIndex = 6;
            this.mediumDiffLvlButton.TabStop = true;
            this.mediumDiffLvlButton.Text = "Średni";
            this.mediumDiffLvlButton.UseVisualStyleBackColor = true;
            this.mediumDiffLvlButton.CheckedChanged += new System.EventHandler(this.Any_SelectedIndexChanged);
            // 
            // easyDiffLvlButton
            // 
            this.easyDiffLvlButton.AutoSize = true;
            this.easyDiffLvlButton.Location = new System.Drawing.Point(18, 35);
            this.easyDiffLvlButton.Name = "easyDiffLvlButton";
            this.easyDiffLvlButton.Size = new System.Drawing.Size(54, 17);
            this.easyDiffLvlButton.TabIndex = 6;
            this.easyDiffLvlButton.TabStop = true;
            this.easyDiffLvlButton.Text = "Łatwy";
            this.easyDiffLvlButton.UseVisualStyleBackColor = true;
            this.easyDiffLvlButton.CheckedChanged += new System.EventHandler(this.Any_SelectedIndexChanged);
            // 
            // decksListBox
            // 
            this.decksListBox.FormattingEnabled = true;
            this.decksListBox.Location = new System.Drawing.Point(566, 12);
            this.decksListBox.Name = "decksListBox";
            this.decksListBox.Size = new System.Drawing.Size(203, 251);
            this.decksListBox.TabIndex = 6;
            this.decksListBox.SelectedIndexChanged += new System.EventHandler(this.decksListBox_SelectedIndexChanged);
            // 
            // uploadOwnDeckButton
            // 
            this.uploadOwnDeckButton.Location = new System.Drawing.Point(641, 269);
            this.uploadOwnDeckButton.Name = "uploadOwnDeckButton";
            this.uploadOwnDeckButton.Size = new System.Drawing.Size(128, 23);
            this.uploadOwnDeckButton.TabIndex = 8;
            this.uploadOwnDeckButton.Text = "Wgraj własną talie ...";
            this.uploadOwnDeckButton.UseVisualStyleBackColor = true;
            this.uploadOwnDeckButton.Click += new System.EventHandler(this.uploadOwnDeckButton_Click);
            // 
            // createServerButton
            // 
            this.createServerButton.Location = new System.Drawing.Point(159, 201);
            this.createServerButton.Name = "createServerButton";
            this.createServerButton.Size = new System.Drawing.Size(127, 23);
            this.createServerButton.TabIndex = 9;
            this.createServerButton.Text = "Utwórz serwer";
            this.createServerButton.UseVisualStyleBackColor = true;
            this.createServerButton.Click += new System.EventHandler(this.createServerButton_Click);

            // 
            // CreateGameServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            
            this.Controls.Add(this.createServerButton);
            this.Controls.Add(this.uploadOwnDeckButton);
            this.Controls.Add(this.decksListBox);
            this.Controls.Add(this.difficultyLevelGroupBox);
            this.Controls.Add(this.gameModeGroupBox);
            this.Name = "CreateGameServerForm";
            this.Load += new System.EventHandler(this.CreateGameServerForm_Load);
            this.Controls.SetChildIndex(this.backButton, 0);
            this.Controls.SetChildIndex(this.portTextBox, 0);
            this.Controls.SetChildIndex(this.addressTextBox, 0);
            this.Controls.SetChildIndex(this.addressLabel, 0);
            this.Controls.SetChildIndex(this.portLabel, 0);
            this.Controls.SetChildIndex(this.gameModeGroupBox, 0);
            this.Controls.SetChildIndex(this.difficultyLevelGroupBox, 0);
            this.Controls.SetChildIndex(this.decksListBox, 0);
            this.Controls.SetChildIndex(this.startGameButton, 0);
            this.Controls.SetChildIndex(this.uploadOwnDeckButton, 0);
            this.Controls.SetChildIndex(this.createServerButton, 0);
            
            this.Controls.SetChildIndex(this.infoTextBox, 0);
            this.Controls.SetChildIndex(this.testButton, 0);
            this.gameModeGroupBox.ResumeLayout(false);
            this.gameModeGroupBox.PerformLayout();
            this.difficultyLevelGroupBox.ResumeLayout(false);
            this.difficultyLevelGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton gameModeNormalButton;
        private System.Windows.Forms.RadioButton gameModeTimeButton;
        private System.Windows.Forms.GroupBox gameModeGroupBox;
        private System.Windows.Forms.GroupBox difficultyLevelGroupBox;
        private System.Windows.Forms.RadioButton customDiffLvlButton;
        private System.Windows.Forms.RadioButton hardDiffLvlButton;
        private System.Windows.Forms.RadioButton mediumDiffLvlButton;
        private System.Windows.Forms.RadioButton easyDiffLvlButton;
        private System.Windows.Forms.ListBox decksListBox;
        private System.Windows.Forms.Button uploadOwnDeckButton;
        private System.Windows.Forms.Button createServerButton;
        //private System.Windows.Forms.Label tooltipLabel;
    }
}
namespace Memory
{
    partial class CreateGameClientForm
    {

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectToServerButton = new System.Windows.Forms.Button();
            this.successfulConnectedLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // connectToServerButton
            // 
            this.connectToServerButton.Location = new System.Drawing.Point(159, 153);
            this.connectToServerButton.Name = "connectToServerButton";
            this.connectToServerButton.Size = new System.Drawing.Size(75, 23);
            this.connectToServerButton.TabIndex = 15;
            this.connectToServerButton.Text = "Podłącz";
            this.connectToServerButton.UseVisualStyleBackColor = true;
            this.connectToServerButton.Click += new System.EventHandler(this.connectToServerButton_Click);
            // 
            // successfulConnectedLabel
            // 
            this.successfulConnectedLabel.AutoSize = true;
            this.successfulConnectedLabel.Location = new System.Drawing.Point(45, 295);
            this.successfulConnectedLabel.Name = "successfulConnectedLabel";
            this.successfulConnectedLabel.Size = new System.Drawing.Size(139, 13);
            this.successfulConnectedLabel.TabIndex = 10;
            this.successfulConnectedLabel.Text = "Pomyślnie połączono z serwerem";
            this.successfulConnectedLabel.Visible = false;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 489);
            this.Controls.Add(this.connectToServerButton);
            this.Controls.Add(this.successfulConnectedLabel);
            this.Name = "CreateGameWindow";
            this.Text = "Memory game - connecting to server";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.Controls.SetChildIndex(this.successfulConnectedLabel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button connectToServerButton;
        private System.Windows.Forms.Label successfulConnectedLabel;
    }
}
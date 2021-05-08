using System;

namespace Memory
{
    partial class GameWindowServerForm
    {
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.startGameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startGameButton
            // 
            this.startGameButton.Location = new System.Drawing.Point(12, 500);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(75, 23);
            this.startGameButton.TabIndex = 0;
            this.startGameButton.Text = "Start";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // GameWindowServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 563);
            this.Controls.Add(this.startGameButton);
            this.Name = "GameWindowServerForm";
            this.Text = "Memory game - server";
            this.Load += new System.EventHandler(this.GameWindowServerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Button startGameButton;
    }
}
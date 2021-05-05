namespace Memory
{
    partial class WelcomeWindowForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        //private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.Button statisticsButton;
        private System.Windows.Forms.Button exitGameButton;
        private System.Windows.Forms.Label label;

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.startGameButton = new System.Windows.Forms.Button();
            this.statisticsButton = new System.Windows.Forms.Button();
            this.exitGameButton = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label.Location = new System.Drawing.Point(90, 43);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(309, 51);
            this.label.TabIndex = 0;
            this.label.Text = "Memory game!";
            this.label.Click += new System.EventHandler(this.label_Click);
            // 
            // startGame
            // 
            this.startGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.startGameButton.Location = new System.Drawing.Point(164, 162);
            this.startGameButton.Name = "startGame";
            this.startGameButton.Size = new System.Drawing.Size(165, 42);
            this.startGameButton.TabIndex = 1;
            this.startGameButton.Text = "Rozpocznij";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // statistics
            // 
            this.statisticsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.statisticsButton.Location = new System.Drawing.Point(164, 236);
            this.statisticsButton.Name = "statistics";
            this.statisticsButton.Size = new System.Drawing.Size(165, 42);
            this.statisticsButton.TabIndex = 2;
            this.statisticsButton.Text = "Statystyki";
            this.statisticsButton.UseVisualStyleBackColor = true;
            this.statisticsButton.Click += new System.EventHandler(this.Statistics_Click);
            // 
            // exitGame
            // 
            this.exitGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.exitGameButton.Location = new System.Drawing.Point(164, 311);
            this.exitGameButton.Name = "exitGame";
            this.exitGameButton.Size = new System.Drawing.Size(165, 42);
            this.exitGameButton.TabIndex = 3;
            this.exitGameButton.Text = "Zakończ";
            this.exitGameButton.UseVisualStyleBackColor = true;
            this.exitGameButton.Click += new System.EventHandler(this.ExitGame_Click);
            // 
            // WelcomeWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(470, 503);
            this.Controls.Add(this.exitGameButton);
            this.Controls.Add(this.statisticsButton);
            this.Controls.Add(this.startGameButton);
            this.Controls.Add(this.label);
            this.Name = "WelcomeWindowForm";
            this.Text = "Memory game";
            this.Load += new System.EventHandler(this.WelcomeWindowForm_Load);
            this.Controls.SetChildIndex(this.label, 0);
            this.Controls.SetChildIndex(this.startGameButton, 0);
            this.Controls.SetChildIndex(this.statisticsButton, 0);
            this.Controls.SetChildIndex(this.exitGameButton, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}


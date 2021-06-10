namespace Memory
{
    partial class CreateOrConnectToServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.connectToServerButton = new System.Windows.Forms.Button();
            this.createServerButton = new System.Windows.Forms.Button();     
            this.SuspendLayout();
            // 
            // connectToServerButton
            // 
            this.connectToServerButton.Location = new System.Drawing.Point(113, 114);
            this.connectToServerButton.Name = "button2";
            this.connectToServerButton.Size = new System.Drawing.Size(137, 54);
            this.connectToServerButton.TabIndex = 1;
            this.connectToServerButton.Text = "Podłącz się do serwera";
            this.connectToServerButton.UseVisualStyleBackColor = true;
            this.connectToServerButton.Click += new System.EventHandler(this.connectToServerButton_Click);
            // 
            // createServerButton
            // 
            this.createServerButton.Location = new System.Drawing.Point(113, 229);
            this.createServerButton.Name = "button1";
            this.createServerButton.Size = new System.Drawing.Size(137, 54);
            this.createServerButton.TabIndex = 0;
            this.createServerButton.Text = "Stwórz serwer";
            this.createServerButton.UseVisualStyleBackColor = true;
            this.createServerButton.Click += new System.EventHandler(this.createServerButton_Click);
            // 
            // CreateOrConnectToServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 450);
            this.Controls.Add(this.createServerButton);
            this.Controls.Add(this.connectToServerButton);
            this.Name = "CreateOrConnectToServerForm";
            this.Text = "Memory game - creating game";
            this.Load += new System.EventHandler(this.CreateOrConnectToServerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button connectToServerButton;
        private System.Windows.Forms.Button createServerButton;
        
    }
}
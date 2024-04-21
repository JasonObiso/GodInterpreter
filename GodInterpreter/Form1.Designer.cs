namespace GodInterpreter
{
    partial class Interface
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
            this.CodeText = new System.Windows.Forms.TextBox();
            this.RunButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CodeText
            // 
            this.CodeText.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.CodeText.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeText.ForeColor = System.Drawing.SystemColors.Window;
            this.CodeText.Location = new System.Drawing.Point(12, 12);
            this.CodeText.Multiline = true;
            this.CodeText.Name = "CodeText";
            this.CodeText.Size = new System.Drawing.Size(776, 394);
            this.CodeText.TabIndex = 0;
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(12, 415);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(776, 23);
            this.RunButton.TabIndex = 1;
            this.RunButton.Text = "RUN INTERPRETER";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RunButton_MouseClick);
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.CodeText);
            this.Name = "Interface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GodInterpreter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CodeText;
        private System.Windows.Forms.Button RunButton;
    }
}


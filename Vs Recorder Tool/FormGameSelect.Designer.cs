namespace Vs_Recorder_Tool
{
    partial class FormGameSelect
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
            this.GameSelectButtonPt = new System.Windows.Forms.RadioButton();
            this.GameSelectButtonHGSS = new System.Windows.Forms.RadioButton();
            this.GameSelectButtonBW = new System.Windows.Forms.RadioButton();
            this.GameSelectButtonB2W2 = new System.Windows.Forms.RadioButton();
            this.GameSelectButtonContinue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GameSelectButtonPt
            // 
            this.GameSelectButtonPt.AutoSize = true;
            this.GameSelectButtonPt.Location = new System.Drawing.Point(33, 28);
            this.GameSelectButtonPt.Name = "GameSelectButtonPt";
            this.GameSelectButtonPt.Size = new System.Drawing.Size(65, 17);
            this.GameSelectButtonPt.TabIndex = 0;
            this.GameSelectButtonPt.TabStop = true;
            this.GameSelectButtonPt.Text = "Platinum";
            this.GameSelectButtonPt.UseVisualStyleBackColor = true;
            this.GameSelectButtonPt.CheckedChanged += new System.EventHandler(this.GameSelectButtonPt_CheckedChanged);
            // 
            // GameSelectButtonHGSS
            // 
            this.GameSelectButtonHGSS.AutoSize = true;
            this.GameSelectButtonHGSS.Location = new System.Drawing.Point(33, 51);
            this.GameSelectButtonHGSS.Name = "GameSelectButtonHGSS";
            this.GameSelectButtonHGSS.Size = new System.Drawing.Size(131, 17);
            this.GameSelectButtonHGSS.TabIndex = 1;
            this.GameSelectButtonHGSS.TabStop = true;
            this.GameSelectButtonHGSS.Text = "HeartGold / SoulSilver";
            this.GameSelectButtonHGSS.UseVisualStyleBackColor = true;
            this.GameSelectButtonHGSS.CheckedChanged += new System.EventHandler(this.GameSelectButtonHGSS_CheckedChanged);
            // 
            // GameSelectButtonBW
            // 
            this.GameSelectButtonBW.AutoSize = true;
            this.GameSelectButtonBW.Location = new System.Drawing.Point(33, 74);
            this.GameSelectButtonBW.Name = "GameSelectButtonBW";
            this.GameSelectButtonBW.Size = new System.Drawing.Size(91, 17);
            this.GameSelectButtonBW.TabIndex = 2;
            this.GameSelectButtonBW.TabStop = true;
            this.GameSelectButtonBW.Text = "Black / White";
            this.GameSelectButtonBW.UseVisualStyleBackColor = true;
            this.GameSelectButtonBW.CheckedChanged += new System.EventHandler(this.GameSelectButtonBW_CheckedChanged);
            // 
            // GameSelectButtonB2W2
            // 
            this.GameSelectButtonB2W2.AutoSize = true;
            this.GameSelectButtonB2W2.Location = new System.Drawing.Point(33, 97);
            this.GameSelectButtonB2W2.Name = "GameSelectButtonB2W2";
            this.GameSelectButtonB2W2.Size = new System.Drawing.Size(109, 17);
            this.GameSelectButtonB2W2.TabIndex = 3;
            this.GameSelectButtonB2W2.TabStop = true;
            this.GameSelectButtonB2W2.Text = "Black 2 / White 2";
            this.GameSelectButtonB2W2.UseVisualStyleBackColor = true;
            this.GameSelectButtonB2W2.CheckedChanged += new System.EventHandler(this.GameSelectButtonB2W2_CheckedChanged);
            // 
            // GameSelectButtonContinue
            // 
            this.GameSelectButtonContinue.Location = new System.Drawing.Point(33, 118);
            this.GameSelectButtonContinue.Name = "GameSelectButtonContinue";
            this.GameSelectButtonContinue.Size = new System.Drawing.Size(75, 23);
            this.GameSelectButtonContinue.TabIndex = 4;
            this.GameSelectButtonContinue.Text = "Continue";
            this.GameSelectButtonContinue.UseVisualStyleBackColor = true;
            this.GameSelectButtonContinue.Click += new System.EventHandler(this.GameSelectButtonContinue_Click);
            // 
            // GameSelectPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 153);
            this.Controls.Add(this.GameSelectButtonContinue);
            this.Controls.Add(this.GameSelectButtonB2W2);
            this.Controls.Add(this.GameSelectButtonBW);
            this.Controls.Add(this.GameSelectButtonHGSS);
            this.Controls.Add(this.GameSelectButtonPt);
            this.Name = "GameSelectPopUp";
            this.Text = "Save File Type";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton GameSelectButtonPt;
        private System.Windows.Forms.RadioButton GameSelectButtonHGSS;
        private System.Windows.Forms.RadioButton GameSelectButtonBW;
        private System.Windows.Forms.RadioButton GameSelectButtonB2W2;
        private System.Windows.Forms.Button GameSelectButtonContinue;
    }
}
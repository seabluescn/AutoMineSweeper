namespace MineSweeper
{
    partial class ShowRankingDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowRankingDialog));
            this.radEasy = new System.Windows.Forms.RadioButton();
            this.radNormal = new System.Windows.Forms.RadioButton();
            this.radHard = new System.Windows.Forms.RadioButton();
            this.radHell = new System.Windows.Forms.RadioButton();
            this.numHellCount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numHellCount)).BeginInit();
            this.SuspendLayout();
            // 
            // radEasy
            // 
            this.radEasy.AutoSize = true;
            this.radEasy.Location = new System.Drawing.Point(130, 71);
            this.radEasy.Name = "radEasy";
            this.radEasy.Size = new System.Drawing.Size(150, 22);
            this.radEasy.TabIndex = 3;
            this.radEasy.TabStop = true;
            this.radEasy.Text = "Easy(20Mines)";
            this.radEasy.UseVisualStyleBackColor = true;
            this.radEasy.CheckedChanged += new System.EventHandler(this.radEasy_CheckedChanged);
            // 
            // radNormal
            // 
            this.radNormal.AutoSize = true;
            this.radNormal.Location = new System.Drawing.Point(130, 131);
            this.radNormal.Name = "radNormal";
            this.radNormal.Size = new System.Drawing.Size(168, 22);
            this.radNormal.TabIndex = 4;
            this.radNormal.TabStop = true;
            this.radNormal.Text = "Normal(30Mines)";
            this.radNormal.UseVisualStyleBackColor = true;
            this.radNormal.CheckedChanged += new System.EventHandler(this.radNormal_CheckedChanged);
            // 
            // radHard
            // 
            this.radHard.AutoSize = true;
            this.radHard.Location = new System.Drawing.Point(130, 202);
            this.radHard.Name = "radHard";
            this.radHard.Size = new System.Drawing.Size(150, 22);
            this.radHard.TabIndex = 5;
            this.radHard.TabStop = true;
            this.radHard.Text = "Hard(40Mines)";
            this.radHard.UseVisualStyleBackColor = true;
            this.radHard.CheckedChanged += new System.EventHandler(this.radHard_CheckedChanged);
            // 
            // radHell
            // 
            this.radHell.AutoSize = true;
            this.radHell.Location = new System.Drawing.Point(130, 278);
            this.radHell.Name = "radHell";
            this.radHell.Size = new System.Drawing.Size(159, 22);
            this.radHell.TabIndex = 6;
            this.radHell.TabStop = true;
            this.radHell.Text = "Hell(50+Mines)";
            this.radHell.UseVisualStyleBackColor = true;
            this.radHell.CheckedChanged += new System.EventHandler(this.radHell_CheckedChanged);
            // 
            // numHellCount
            // 
            this.numHellCount.Enabled = false;
            this.numHellCount.Location = new System.Drawing.Point(348, 278);
            this.numHellCount.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numHellCount.Name = "numHellCount";
            this.numHellCount.Size = new System.Drawing.Size(83, 28);
            this.numHellCount.TabIndex = 7;
            this.numHellCount.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numHellCount.ValueChanged += new System.EventHandler(this.numHellCount_ValueChanged);
            // 
            // ShowRankingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 385);
            this.Controls.Add(this.numHellCount);
            this.Controls.Add(this.radHell);
            this.Controls.Add(this.radHard);
            this.Controls.Add(this.radNormal);
            this.Controls.Add(this.radEasy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowRankingDialog";
            this.Text = "DifficultGradeSeeting";
            this.Load += new System.EventHandler(this.ShowRankingDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numHellCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton radEasy;
        private System.Windows.Forms.RadioButton radNormal;
        private System.Windows.Forms.RadioButton radHard;
        private System.Windows.Forms.RadioButton radHell;
        private System.Windows.Forms.NumericUpDown numHellCount;
    }
}
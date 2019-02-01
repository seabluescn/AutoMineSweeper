namespace MineSweeper
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.pictureBoxMain = new System.Windows.Forms.PictureBox();
            this.picState = new System.Windows.Forms.PictureBox();
            this.lblTimeCount = new System.Windows.Forms.Label();
            this.lblRanking = new System.Windows.Forms.LinkLabel();
            this.lblMineCount = new System.Windows.Forms.Label();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.picDebug = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkAbout = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDebug)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxMain.Location = new System.Drawing.Point(18, 92);
            this.pictureBoxMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxMain.Name = "pictureBoxMain";
            this.pictureBoxMain.Size = new System.Drawing.Size(899, 539);
            this.pictureBoxMain.TabIndex = 3;
            this.pictureBoxMain.TabStop = false;
            this.pictureBoxMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMain_MouseClick);
            // 
            // picState
            // 
            this.picState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picState.Image = global::MineSweeper.Properties.Resources.Face_Success;
            this.picState.Location = new System.Drawing.Point(382, 18);
            this.picState.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picState.Name = "picState";
            this.picState.Size = new System.Drawing.Size(59, 59);
            this.picState.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picState.TabIndex = 4;
            this.picState.TabStop = false;
            this.picState.Click += new System.EventHandler(this.picState_Click);
            // 
            // lblTimeCount
            // 
            this.lblTimeCount.AutoSize = true;
            this.lblTimeCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTimeCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTimeCount.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTimeCount.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblTimeCount.Location = new System.Drawing.Point(150, 26);
            this.lblTimeCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimeCount.Name = "lblTimeCount";
            this.lblTimeCount.Size = new System.Drawing.Size(73, 41);
            this.lblTimeCount.TabIndex = 5;
            this.lblTimeCount.Text = "001";
            // 
            // lblRanking
            // 
            this.lblRanking.AutoSize = true;
            this.lblRanking.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRanking.Location = new System.Drawing.Point(820, 32);
            this.lblRanking.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRanking.Name = "lblRanking";
            this.lblRanking.Size = new System.Drawing.Size(73, 28);
            this.lblRanking.TabIndex = 6;
            this.lblRanking.TabStop = true;
            this.lblRanking.Text = "Grade";
            this.lblRanking.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblRanking_LinkClicked);
            // 
            // lblMineCount
            // 
            this.lblMineCount.AutoSize = true;
            this.lblMineCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMineCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblMineCount.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMineCount.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblMineCount.Location = new System.Drawing.Point(694, 27);
            this.lblMineCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMineCount.Name = "lblMineCount";
            this.lblMineCount.Size = new System.Drawing.Size(55, 41);
            this.lblMineCount.TabIndex = 7;
            this.lblMineCount.Text = "01";
            // 
            // timerMain
            // 
            this.timerMain.Interval = 1000;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // picDebug
            // 
            this.picDebug.Location = new System.Drawing.Point(940, 92);
            this.picDebug.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picDebug.Name = "picDebug";
            this.picDebug.Size = new System.Drawing.Size(300, 180);
            this.picDebug.TabIndex = 8;
            this.picDebug.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(20, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "Game Time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(540, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 24);
            this.label2.TabIndex = 10;
            this.label2.Text = "Remain Mines:";
            // 
            // linkAbout
            // 
            this.linkAbout.AutoSize = true;
            this.linkAbout.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkAbout.Location = new System.Drawing.Point(936, 34);
            this.linkAbout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkAbout.Name = "linkAbout";
            this.linkAbout.Size = new System.Drawing.Size(59, 20);
            this.linkAbout.TabIndex = 11;
            this.linkAbout.TabStop = true;
            this.linkAbout.Text = "About";
            this.linkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAbout_LinkClicked);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 645);
            this.Controls.Add(this.linkAbout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picDebug);
            this.Controls.Add(this.lblMineCount);
            this.Controls.Add(this.lblRanking);
            this.Controls.Add(this.lblTimeCount);
            this.Controls.Add(this.picState);
            this.Controls.Add(this.pictureBoxMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "MineSweeper";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDebug)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.PictureBox picState;
        private System.Windows.Forms.Label lblTimeCount;
        private System.Windows.Forms.LinkLabel lblRanking;
        private System.Windows.Forms.Label lblMineCount;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.PictureBox picDebug;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkAbout;
    }
}


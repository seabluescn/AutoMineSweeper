using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class ShowRankingDialog : Form
    {
        private Grade _DifficultGrade = Grade.Easy;
        private int _MinesCount;

        public Grade DifficultGrade { get => _DifficultGrade; set => _DifficultGrade = value; }
        public int MinesCount { get => _MinesCount; set => _MinesCount = value; }

        public ShowRankingDialog()
        {
            InitializeComponent();
        }

        private void ShowRankingDialog_Load(object sender, EventArgs e)
        {
           switch(DifficultGrade)
            {
                case Grade.Easy:
                    this.radEasy.Checked = true;                   
                    break;
                case Grade.Normal:
                    this.radNormal.Checked = true;                   
                    break;
                case Grade.Hard:
                    this.radHard.Checked = true;                   
                    break;
                case Grade.Hell:
                    this.radHell.Checked = true;
                    if (MinesCount < 50) MinesCount = 50;
                    this.numHellCount.Value = MinesCount;
                    break;
            }
        }      

        private void radEasy_CheckedChanged(object sender, EventArgs e)
        {
            if (radEasy.Checked)
            {
                DifficultGrade = Grade.Easy;
                MinesCount = (int)DifficultGrade;
                this.numHellCount.Enabled = false;
            }
        }

        private void radNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (radNormal.Checked)
            {
                DifficultGrade = Grade.Normal;
                MinesCount = (int)DifficultGrade;
                this.numHellCount.Enabled = false;
            }
        }

        private void radHard_CheckedChanged(object sender, EventArgs e)
        {
            if (radHard.Checked)
            {
                DifficultGrade = Grade.Hard;
                MinesCount = (int)DifficultGrade;
                this.numHellCount.Enabled = false;
            }
        }

        private void radHell_CheckedChanged(object sender, EventArgs e)
        {
            if(radHell.Checked)
            {
                DifficultGrade = Grade.Hell;
                this.numHellCount.Enabled = true;
            }
        }

        private void numHellCount_ValueChanged(object sender, EventArgs e)
        {
            MinesCount = (int)this.numHellCount.Value;
        }
    }
}

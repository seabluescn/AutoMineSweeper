using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class FormMain : Form
    {
        private const int MaxX = 20;
        private const int MaxY = 12;

        Grade diffcultGrade = Grade.Easy;
        private  int TotalMinesMax = 30;
        private int TotalMines=0;

        private const int RectWidth = 30;
        private const int RectHeighth = 30;

        private GameState GameState = GameState.Success;
        private int TimeCount = 0;
        private int MineCount = 0;

        MineState[,] MineStateMap = new MineState[MaxX, MaxY];
        BoxState[,] BoxStateMap = new BoxState[MaxX, MaxY];


        public FormMain()
        {
            InitializeComponent();
            
            try
            {
                diffcultGrade = (Grade)Properties.Settings.Default.DiffcultGrade;
                TotalMinesMax = Properties.Settings.Default.HellMinesCount;
            }
            catch
            {
                diffcultGrade = Grade.Easy;
                Properties.Settings.Default.Save();
            }

            if (diffcultGrade!= Grade.Hell)
            {
                TotalMinesMax = (int)diffcultGrade;
            }            

            RestartGame();
        }

        private void picState_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            this.GameState = GameState.Ready;
            this.picState.Image = Properties.Resources.Face_Ready;
            TimeCount = 0;
            MineCount = 0;

            //Init MineState
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    MineStateMap[x, y] = MineState.HAVENOT;
                }
            }

            TotalMines = TotalMinesMax;
            Random random = new Random();
            for (int count = 0; count < TotalMines; count++)
            {
                int x = random.Next(MaxX);
                int y = random.Next(MaxY);
                MineStateMap[x, y] = MineState.HAVE;
            }

            TotalMines = 0;
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    if (MineStateMap[x, y] == MineState.HAVE)
                    {
                        TotalMines++;
                    }
                }
            }

            //init BoxState
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    BoxStateMap[x, y] = BoxState.Unknow;
                }
            }

            RedrawImage();
            RedrawDebugImage();

            this.timerMain.Enabled = true;
        }

        private void RedrawDebugImage()
        {
            int width = 10 * MaxX;
            int height = 10 * MaxY;
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);

            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {                   
                    Bitmap bmp = null;
                    if(MineStateMap[x, y] == MineState.HAVE)
                    {
                        bmp = Properties.Resources.Box_Mine;
                    }
                    else
                    {
                        bmp = Properties.Resources.Box_None;
                    }

                    g.DrawImage(bmp, x * 10, y * 10, 10, 10);
                }
            }

            this.picDebug.Image?.Dispose();
            this.picDebug.Image = bitmap;
            g.Dispose();
        }

        private void RedrawImage()
        {
            int width = RectWidth * MaxX;
            int height = RectHeighth * MaxY;
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);

            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    Bitmap bmp=null;
                    switch(BoxStateMap[x,y])
                    {
                        case BoxState.Unknow:
                            bmp = Properties.Resources.Box_Unknow;
                            break;
                        case BoxState.None:
                            bmp = Properties.Resources.Box_None;
                            break;
                        case BoxState.One:
                            bmp = Properties.Resources.Box_One;
                            break;
                        case BoxState.Two:
                            bmp = Properties.Resources.Box_Two;
                            break;
                        case BoxState.Three:
                            bmp = Properties.Resources.Box_Three;
                            break;
                        case BoxState.Four:
                            bmp = Properties.Resources.Box_Four;
                            break;
                        case BoxState.Five:
                            bmp = Properties.Resources.Box_Five;
                            break;
                        case BoxState.Six:
                            bmp = Properties.Resources.Box_Six;
                            break;
                        case BoxState.Seven:
                            bmp = Properties.Resources.Box_Seven;
                            break;
                        case BoxState.Eight:
                            bmp = Properties.Resources.Box_Eight;
                            break;
                        case BoxState.MineBoom:
                            bmp = Properties.Resources.Box_Mine_Boom;
                            break;
                        case BoxState.Mine:
                            bmp = Properties.Resources.Box_Mine;
                            break;
                        case BoxState.Flag:
                            bmp = Properties.Resources.Box_Flag;
                            break;
                    }

                    g.DrawImage(bmp, x * RectWidth, y * RectHeighth, RectWidth, RectHeighth);
                }

                this.lblMineCount.Text = (TotalMines - MineCount).ToString("D2");
            }

            this.pictureBoxMain.Image?.Dispose();
            this.pictureBoxMain.Image = bitmap;
            g.Dispose();
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            TimeCount++;
            this.lblTimeCount.Text = TimeCount.ToString("D3");

            if(TimeCount>=999)
            {
                GameFail();
            }
        }

        private void GameFail()
        {
            this.GameState = GameState.Fail;
            this.picState.Image = Properties.Resources.Face_Fail;
            this.timerMain.Enabled = false;
        }

        private void GameSuccess()
        {
            this.GameState = GameState.Success;
            this.picState.Image = Properties.Resources.Face_Success;
            this.timerMain.Enabled = false;            
        }             

        private void pictureBoxMain_MouseClick(object sender, MouseEventArgs e)
        {
            if(GameState!= GameState.Ready)
            {
                return;
            }

            int x = e.X / RectWidth;
            int y = e.Y / RectHeighth;
            Debug.WriteLine($"Mouse Click:({x},{y})");

            if(x<0||x>=MaxX||y<0||y>=MaxY)
            {
                Debug.WriteLine("offset error!");
                return;
            }

            if(e.Button== MouseButtons.Left)
            {
                if (BoxStateMap[x, y] == BoxState.Unknow )
                {
                    if (MineStateMap[x, y] == MineState.HAVE)
                    {
                        BoxStateMap[x, y] = BoxState.MineBoom;
                        GameFail();
                    }
                    else
                    {
                        OpenThisBox(x, y);                                   
                    }

                    RedrawImage();
                    return;
                }               
            }

            if (e.Button == MouseButtons.Right)
            {
                if (BoxStateMap[x, y] == BoxState.Unknow && MineCount < TotalMines)
                {
                    BoxStateMap[x, y] = BoxState.Flag;
                    MineCount++;

                    //判断胜利
                    if(CalcSeccessed())
                    {
                        GameSuccess();
                    }

                    RedrawImage();
                    return;
                }

                if (BoxStateMap[x, y] == BoxState.Flag)
                {
                    BoxStateMap[x, y] = BoxState.Unknow;
                    MineCount--;
                    RedrawImage();
                    return;
                }
            }
        }

        private int CalcMinesCount(int locx, int locy)
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(locx - 1, locy - 1));
            points.Add(new Point(locx - 1, locy));
            points.Add(new Point(locx - 1, locy + 1));
            points.Add(new Point(locx, locy - 1));
            points.Add(new Point(locx, locy + 1));
            points.Add(new Point(locx + 1, locy - 1));
            points.Add(new Point(locx + 1, locy));
            points.Add(new Point(locx + 1, locy + 1));

            int count = 0;
            foreach (Point p in points)
            {
                int x = p.X;
                int y = p.Y;

                if (x >= 0 && x < MaxX && y >= 0 && y < MaxY && MineStateMap[x, y] == MineState.HAVE)
                {
                    count++;
                }
            }

            return count;
        }

        private bool CalcSeccessed()
        {
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    if(MineStateMap[x,y]== MineState.HAVE&&BoxStateMap[x,y]!= BoxState.Flag)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void OpenThisBox(int locx, int locy)
        {
            Debug.WriteLine($"OpenNoneBox:({locx},{locy})");

            if (locx >= 0 && locx < MaxX && locy >= 0 && locy < MaxY && BoxStateMap[locx, locy] == BoxState.Unknow)
            {
                int mines = CalcMinesCount(locx, locy);

                switch (mines)
                {
                    case 0: BoxStateMap[locx, locy] = BoxState.None; break;
                    case 1: BoxStateMap[locx, locy] = BoxState.One; break;
                    case 2: BoxStateMap[locx, locy] = BoxState.Two; break;
                    case 3: BoxStateMap[locx, locy] = BoxState.Three; break;
                    case 4: BoxStateMap[locx, locy] = BoxState.Four; break;
                    case 5: BoxStateMap[locx, locy] = BoxState.Five; break;
                    case 6: BoxStateMap[locx, locy] = BoxState.Six; break;
                    case 7: BoxStateMap[locx, locy] = BoxState.Seven; break;
                    case 8: BoxStateMap[locx, locy] = BoxState.Eight; break;
                }

                if (mines == 0)
                {
                    List<Point> points = new List<Point>();
                    points.Add(new Point(locx - 1, locy - 1));
                    points.Add(new Point(locx - 1, locy));
                    points.Add(new Point(locx - 1, locy + 1));
                    points.Add(new Point(locx, locy - 1));
                    points.Add(new Point(locx, locy + 1));
                    points.Add(new Point(locx + 1, locy - 1));
                    points.Add(new Point(locx + 1, locy));
                    points.Add(new Point(locx + 1, locy + 1));

                    foreach (Point p in points)
                    {
                        int x = p.X;
                        int y = p.Y;

                        OpenThisBox(x, y); 
                    }
                }
            }
           
        }

        private void lblRanking_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {   
            ShowRankingDialog showRankingDialog = new ShowRankingDialog();
            showRankingDialog.DifficultGrade = diffcultGrade;
            showRankingDialog.MinesCount = TotalMinesMax;

            showRankingDialog.ShowDialog();

            diffcultGrade = showRankingDialog.DifficultGrade;
            TotalMinesMax = showRankingDialog.MinesCount;

            Properties.Settings.Default.DiffcultGrade = (int)diffcultGrade;
            Properties.Settings.Default.HellMinesCount = TotalMinesMax;
            Properties.Settings.Default.Save();

            RestartGame();
        }

        private void linkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please mail to:seabluescn@163.com if you have any question.", "About");
        }
    }
}

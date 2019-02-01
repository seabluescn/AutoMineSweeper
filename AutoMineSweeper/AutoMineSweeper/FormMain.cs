using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AutoMineSweeper
{
    public partial class FormMain : Form
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, out Rect lpRect);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        const int MOUSEEVENTF_MOVE = 0x0001; //移动鼠标
        const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下
        const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下
        const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下
        const int MOUSEEVENTF_MIDDLEUP = 0x0040; //模拟鼠标中键抬起
        const int MOUSEEVENTF_ABSOLUTE = 0x8000; //标示是否采用绝对坐标

        public FormMain()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.timer.Enabled = true;
            this.txtInfo.Text = "";

            if (chkAutoRestart.Checked)
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            GameState gameState = GetGameState();

            if (gameState != GameState.Ready)
            {
                Rect rect = GetWindowRect();
                int left = rect.Left;
                int top = rect.Top;

                int clickPointx = (left + 264 + 20) * 65535 / Screen.PrimaryScreen.Bounds.Width;
                int clickPointy = (top + 44 + 20) * 65535 / Screen.PrimaryScreen.Bounds.Height;

                mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, clickPointx, clickPointy, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            Recognize();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Recognize();
        }

        private Rect GetWindowRect()
        {
            Process[] processes = Process.GetProcesses();
            Process process = null;
            for (int i = 0; i < processes.Length - 1; i++)
            {
                process = processes[i];
                if (process.MainWindowTitle == "MineSweeper")
                {
                    break;
                }
                process = null;
            }

            if (process == null)
            {
                ShowDebugInfo("\r\nNot Find MineWreeper Window!");
                this.timer.Enabled = false;
                return new Rect();
            }

            Rect rect = new Rect();
            GetWindowRect(process.MainWindowHandle, out rect);

            return rect;
        }

        private GameState GetGameState()
        {
            GameState gameState = GameState.Ready;

            Bitmap bitmapFace = new Bitmap(40, 40);
            using (Graphics graphics = Graphics.FromImage(bitmapFace))
            {
                Rect rect = GetWindowRect();

                graphics.CopyFromScreen(rect.Left + 264, rect.Top + 44, 0, 0, new Size(40, 40));
                this.picFace.Image = bitmapFace;

                Color face = bitmapFace.GetPixel(20, 16);

                //Debug.WriteLine($"face:{face.R},{face.G},{face.B}");

                if (face.R == 212 && face.G == 9 && face.B == 27)
                {
                    gameState = GameState.Fail;
                    ShowDebugInfo("\r\nGame fail!");
                }

                if (face.R == 0 && face.G == 166 && face.B == 240)
                {
                    gameState = GameState.Fail;
                    ShowDebugInfo("\r\nGame success!");
                }
            }

            return gameState;
        }

        private void Recognize()
        {
            //常量
            int MaxX = 20;
            int MaxY = 12;
            int RectWidth = 30;

            //获取窗口位置
            Rect rect = GetWindowRect();

            int left = rect.Left;
            int top = rect.Top;

            //获取游戏状态(笑脸)
            GameState gameState = GetGameState();

            //非游戏状态
            if (gameState != GameState.Ready)
            {
                ShowDebugInfo("\r\nGame over!");
                this.timer.Enabled = false;
                return;
            }


            //截取游戏主要区域

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            int centerleft = 21;
            int centertop = 93;
            int centerwidth = MaxX * RectWidth;
            int centerheight = MaxY * RectWidth;
           
            Bitmap bitmapCenter = new Bitmap(centerwidth, centerheight);
            using (Graphics graphics = Graphics.FromImage(bitmapCenter))
            {
                graphics.CopyFromScreen(left + centerleft, top + centertop, 0, 0, new Size(centerwidth, centerheight));
                this.pictureBox1.Image = bitmapCenter;
            }

            //识别

            MineState[,] MineMap = new MineState[MaxX, MaxY];

            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    Color center = bitmapCenter.GetPixel(x * RectWidth + RectWidth / 2, y * RectWidth + RectWidth / 2);

                    if (center.R == 220 && center.G == 220 && center.B == 220)
                    {
                        MineMap[x, y] = MineState.Unknow;
                    }
                    else if (center.R == 233 && center.G == 233 && center.B == 233)
                    {
                        MineMap[x, y] = MineState.None;
                    }
                    else if (center.R == 90 && center.G == 219 && center.B == 117)
                    {
                        MineMap[x, y] = MineState.One;
                    }
                    else if (center.R == 8 && center.G == 126 && center.B == 33)
                    {
                        MineMap[x, y] = MineState.Two;
                    }
                    else if (center.R == 36 && center.G == 70 && center.B == 243)
                    {
                        MineMap[x, y] = MineState.Three;
                    }
                    else if (center.R == 27 && center.G == 48 && center.B == 155)
                    {
                        MineMap[x, y] = MineState.Four;
                    }
                    else if (center.R == 178 && center.G == 82 && center.B == 209)
                    {
                        MineMap[x, y] = MineState.Five;
                    }
                    else if (center.R == 228 && center.G == 141 && center.B == 28)
                    {
                        MineMap[x, y] = MineState.Six;
                    }
                    else if (center.R == 192 && center.G == 30 && center.B == 227)
                    {
                        MineMap[x, y] = MineState.Seven;
                    }
                    else if (center.R == 218 && center.G == 8 && center.B == 186)
                    {
                        MineMap[x, y] = MineState.Eight;
                    }
                    else if (center.R == 244 && center.G == 35 && center.B == 50)
                    {
                        MineMap[x, y] = MineState.Mine;
                    }
                    else
                    {
                        ShowDebugInfo($"\r\nRecognize Error!({x},{y}):{center.R},{center.G},{center.B}");
                        this.timer.Enabled = false;
                        return;
                    }
                }
            }

            //绘制识别结果
            Bitmap bitmapDebug = new Bitmap(centerwidth, centerheight);
            using (Graphics graphics = Graphics.FromImage(bitmapDebug))
            {
                string s = "";

                for (int x = 0; x < MaxX; x++)
                    for (int y = 0; y < MaxY; y++)
                    {
                        switch (MineMap[x, y])
                        {
                            case MineState.Unknow: s = "?"; break;
                            case MineState.None: s = "0"; break;
                            case MineState.One: s = "1"; break;
                            case MineState.Two: s = "2"; break;
                            case MineState.Three: s = "3"; break;
                            case MineState.Four: s = "4"; break;
                            case MineState.Five: s = "5"; break;
                            case MineState.Six: s = "6"; break;
                            case MineState.Seven: s = "7"; break;
                            case MineState.Eight: s = "8"; break;
                            case MineState.Mine: s = "9"; break;
                            default: s = "X"; break;
                        }
                        graphics.DrawString(s, new Font("宋体", 12), Brushes.Black, x * RectWidth, y * RectWidth);
                    }

                this.pictureBox2.Image = bitmapDebug;
            }
            

            //下面开始计算应该点击的点
            List<ClickEvent> clickEventList = new List<ClickEvent>();
            bool FindGoodPoint = false;   //如果找到合适的点，就完成本次运算


            //第一步：基础算法...计算点击位置
            Debug.WriteLine("1基础算法...");
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    int MineCount = (int)MineMap[x, y];

                    if (MineCount >= 1)
                    {
                        List<BoxLocation> unknowBoxs = SearchMines(x, y, MaxX, MaxY, MineState.Unknow, MineMap);    //附近未开盒子
                        List<BoxLocation> MineBoxs = SearchMines(x, y, MaxX, MaxY, MineState.Mine, MineMap);        //附件已确定的雷

                        if (unknowBoxs.Count > 0)
                        {
                            //主要算法1：数字和周围已经标记的雷数一致，所有未知位置都不是雷，左键点开
                            if (MineBoxs.Count == MineCount)
                            {
                                clickEventList.Clear();
                                foreach (var box in unknowBoxs)
                                {
                                    clickEventList.Add(new ClickEvent(box.LocationX, box.LocationY, ClickType.LeftClick));
                                }
                                FindGoodPoint = true;
                                break;
                            }

                            //主要算法2：中心数字=未知位置数量+周围已经标记的雷数 ：所有未知位置为雷，右键标记
                            if (MineBoxs.Count + unknowBoxs.Count == MineCount)
                            {
                                clickEventList.Clear();
                                foreach (var box in unknowBoxs)
                                {
                                    clickEventList.Add(new ClickEvent(box.LocationX, box.LocationY, ClickType.RightClick));
                                }
                                FindGoodPoint = true;
                                break;
                            }
                        }
                    }
                }

                if (FindGoodPoint)
                {
                    break;
                }
            }
            

            //第二步：补充算法.....第一次没有计算出合适的点，用复杂算法再算一次
            if (!FindGoodPoint)
            {
                Debug.WriteLine("2补充算法....");

                //计算
                List<UnknowBoxSum> UnknowBoxSumList = new List<UnknowBoxSum>();

                for (int locx = 0; locx < MaxX; locx++)
                {
                    for (int locy = 0; locy < MaxY; locy++)
                    {
                        int MineCount = (int)MineMap[locx, locy];

                        if (MineCount >= 1 && MineCount <= 6)
                        {
                            List<BoxLocation> unknowBoxs = SearchMines(locx, locy, MaxX, MaxY, MineState.Unknow, MineMap);    //附近未开盒子
                            List<BoxLocation> MineBoxs = SearchMines(locx, locy, MaxX, MaxY, MineState.Mine, MineMap);        //附件已确定的雷

                            if (unknowBoxs.Count >= 2)
                            {
                                UnknowBoxSum unknowBoxSum = new UnknowBoxSum();
                                unknowBoxSum.Boxes = unknowBoxs;
                                unknowBoxSum.Sum = MineCount - MineBoxs.Count;
                                UnknowBoxSumList.Add(unknowBoxSum);
                            }
                        }
                    }
                }

                Debug.WriteLine($"UnknowBoxSumList={UnknowBoxSumList.Count}");

                if (UnknowBoxSumList.Count > 0)
                {

                    //匹配
                    for (int locx = 0; locx < MaxX; locx++)
                    {
                        for (int locy = 0; locy < MaxY; locy++)
                        {
                            int MineCount = (int)MineMap[locx, locy];

                            if (MineCount >= 1 && MineCount <= 8)
                            {
                                List<BoxLocation> unknowBoxs = SearchMines(locx, locy, MaxX, MaxY, MineState.Unknow, MineMap);    //附近未开盒子
                                List<BoxLocation> MineBoxs = SearchMines(locx, locy, MaxX, MaxY, MineState.Mine, MineMap);        //附件已确定的雷

                                if (unknowBoxs.Count >= 2)
                                {
                                    foreach (var UnknowBoxSum in UnknowBoxSumList)
                                    {
                                        if (unknowBoxs.Count - UnknowBoxSum.Boxes.Count == 1)
                                        {
                                            if (UnknowBoxSum.MatchBox(unknowBoxs))   //先判断是否匹配
                                            {
                                                //数字-雷==Sum：必不是雷
                                                if ((MineCount - MineBoxs.Count - UnknowBoxSum.Sum) == 0)
                                                {

                                                    //取出
                                                    BoxLocation box = UnknowBoxSum.GetNotBelongBox(unknowBoxs);

                                                    Debug.WriteLine($"Match（必不是雷）:({locx},{locy})->({box.LocationX},{box.LocationY})");

                                                    clickEventList.Add(new ClickEvent(box.LocationX, box.LocationY, ClickType.LeftClick));


                                                    FindGoodPoint = true;
                                                    break;
                                                }

                                                //数字-减雷-Sum=1：必是雷
                                                if ((MineCount - MineBoxs.Count - UnknowBoxSum.Sum) == 1)
                                                {

                                                    //取出
                                                    BoxLocation box = UnknowBoxSum.GetNotBelongBox(unknowBoxs);
                                                    Debug.WriteLine($"Match（必是雷）:({locx},{locy})->({box.LocationX},{box.LocationY})");

                                                    clickEventList.Add(new ClickEvent(box.LocationX, box.LocationY, ClickType.RightClick));

                                                    FindGoodPoint = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (FindGoodPoint)
                            {
                                break;
                            }
                        }

                        if (FindGoodPoint)
                        {
                            break;
                        }
                    }

                }
            }


            //第三步：实在没有找到合适的点，只能随机点开
            if (!FindGoodPoint)
            {
                Debug.WriteLine("3随机点开...");

                float[,] ProbabilityMap = new float[MaxX, MaxY];

                //计算最小概率
                float MinProbability = 1;

                for (int x = 0; x < MaxX; x++)
                {
                    for (int y = 0; y < MaxY; y++)
                    {
                        if (MineMap[x, y] == MineState.Unknow)
                        {
                            ProbabilityMap[x, y] = CalcProbability(x, y, MaxX, MaxY, MineMap);

                            if (ProbabilityMap[x, y] < MinProbability)
                            {
                                MinProbability = ProbabilityMap[x, y];
                            }
                        }
                    }
                }
                Debug.WriteLine($"MinProbability={MinProbability}");

                //取出最小概率的点
                for (int x = 0; x < MaxX; x++)
                {
                    for (int y = 0; y < MaxY; y++)
                    {
                        if (MineMap[x, y] == MineState.Unknow && Math.Abs(ProbabilityMap[x, y] - MinProbability) < 0.001)
                        {
                            clickEventList.Add(new ClickEvent(x, y, ClickType.LeftClick));
                        }
                    }
                }

                /*
                foreach (var clickEvent in clickEventList)
                {
                    Debug.WriteLine($"selected clickEvent:{clickEvent}");
                }
                */

                int Count = clickEventList.Count;
                ShowDebugInfo($"\r\nRandom Select:Count={Count}");

                if (Count == 0)
                {
                    ShowDebugInfo("\r\n clickEventList.Count == 0,I can't do anything!");
                    this.timer.Enabled = false;
                    return;
                }
                else
                {
                    if (Count == 1)
                    {
                        ClickEvent clickEvent = clickEventList[0];
                    }
                    else
                    {   
                        Random random = new Random();
                        ClickEvent clickEvent = clickEventList[random.Next(Count)];
                        clickEventList.Clear();
                        clickEventList.Add(clickEvent);
                    }
                }
            }


            //点击屏幕
            ClickScreen(RectWidth, left, top, centerleft, centertop, clickEventList);
        }
                          

        //查找某个点附件某种型号的方块
        private List<BoxLocation> SearchMines(int locx, int locy, int MaxX, int MaxY, MineState mineType, MineState[,] mineMaps)
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

            List<BoxLocation> mines = new List<BoxLocation>();
            foreach (Point p in points)
            {
                int x = p.X;
                int y = p.Y;

                if (x >= 0 && x < MaxX && y >= 0 && y < MaxY && mineMaps[x, y] == mineType)
                {
                    mines.Add(new BoxLocation(x, y, mineType));
                }
            }

            return mines;
        }

        //计算概率
        private float CalcProbability(int locx, int locy, int MaxX, int MaxY, MineState[,] MineMap)
        {
            //Debug.WriteLine($"Probability:({locx},{locy}):Begin");

            float AverageProbability = 0.21f;    //平均概率:在一片空白内点击的概率（经验数据，选择0.21或0.19，根据难度）                       

            float Probability;

            if (MatchMinesType(locx, locy, MaxX, MaxY, MineMap))
            {
                Probability = AverageProbability;
            }
            else
            {
                Probability = 0.1f;

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

                    if (x >= 0 && x < MaxX && y >= 0 && y < MaxY)
                    {
                        int mines = (int)MineMap[x, y];
                        if (mines >= 1 && mines <= 8)
                        {
                            int flags = SearchMines(x, y, MaxX, MaxY, MineState.Mine, MineMap).Count;
                            int unkows = SearchMines(x, y, MaxX, MaxY, MineState.Unknow, MineMap).Count;
                            float currentProbability = (float)(mines - flags) / unkows;

                            if (currentProbability > Probability)
                            {
                                Probability = currentProbability;
                            }

                            //Debug.WriteLine($"({x},{y}):mines={mines},flags={flags},unkows={unkows},currentProbability={currentProbability}");
                        }
                    }
                }
            }

            //Debug.WriteLine($"Probability:({locx},{locy}):{Probability}");

            return Probability;
        }

        //判断是否没有有数字的盒子
        private Boolean MatchMinesType(int locx, int locy, int MaxX, int MaxY, MineState[,] mineMaps)
        {
            bool match = true;

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

                if (x >= 0 && x < MaxX && y >= 0 && y < MaxY && mineMaps[x, y] != MineState.Unknow && mineMaps[x, y] != MineState.Mine)
                {
                    match = false;
                }
            }

            return match;
        }

        //点击屏幕
        private void ClickScreen(int RectWidth, int left, int top, int centerleft, int centertop, List<ClickEvent> clickEventList)
        {
            foreach (var clickEvent in clickEventList)
            {
                ShowDebugInfo("Click:" + clickEvent.ToString());

                int clickPointX = (left + centerleft + clickEvent.LocalX * RectWidth + RectWidth / 2) * 65535 / Screen.PrimaryScreen.Bounds.Width;
                int clickPointY = (top + centertop + clickEvent.LocalY * RectWidth + RectWidth / 2) * 65535 / Screen.PrimaryScreen.Bounds.Height;


                mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, clickPointX, clickPointY, 0, 0);

                if (clickEvent.ClickType == ClickType.LeftClick)
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }
                else
                {
                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                }
            }
        }

        private void ShowDebugInfo(string info)
        {
            //Debug.WriteLine(info);
            this.txtInfo.Text += "\r\n" + info;
        }
    }
}


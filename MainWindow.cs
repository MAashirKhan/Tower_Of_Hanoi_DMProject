using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tower_Of_Hanoi_DMProject
{
    public partial class MainWindow : Form
    {
        //Creating STACK for different point
        static public Stack<int> A = new Stack<int>();
        static public Stack<int> B = new Stack<int>();
        static public Stack<int> C = new Stack<int>();

        //Creating List for moveinfo 
        static public List<MoveInfo> algorithms_steps = new List<MoveInfo>();
        
        static int currentStep = 0;
        static int countA = 0, countB = 0, countC = 0;
        
        //Methods For PrintStack
        static public void PrintStack() 
        {
            if (countA != A.Count
                || countB != B.Count
                || countC != C.Count)
            {
                int diffA = A.Count - countA;
                int diffB = B.Count - countB;
                int diffC = C.Count - countC;
                if (diffA == 1) 
                {
                    if (diffB == -1)
                    {
                        algorithms_steps.Add(new MoveInfo("B", "A", A.Peek()));
                    }
                    else
                    {
                        algorithms_steps.Add(new MoveInfo("C", "A", A.Peek()));
                    }
                }
                else if (diffB == 1)
                {
                    if (diffA == -1)
                    {
                        algorithms_steps.Add(new MoveInfo("A", "B", B.Peek()));
                    }
                    else
                    {
                        algorithms_steps.Add(new MoveInfo("C", "B", B.Peek()));
                    }
                }
                else 
                {
                    if (diffA == -1)
                    {
                        algorithms_steps.Add(new MoveInfo("A", "C", C.Peek()));
                    }
                    else
                    {
                        algorithms_steps.Add(new MoveInfo("B", "C", C.Peek()));
                    }
                }
                countA = A.Count;
                countB = B.Count;
                countC = C.Count;
                Console.WriteLine();
            }
        }

        static int movecount = 0;
        //Method For Solving 2 Disc TOH
        public static void Solve2DISCTOH(Stack<int> source, Stack<int> temp, Stack<int> dest) 
        {
            temp.Push(source.Pop());
            movecount++;
            PrintStack();
            dest.Push(source.Pop());
            movecount++;
            PrintStack();
            dest.Push(temp.Pop());
            movecount++;
            PrintStack();
        }
        // Methods For Solving TOH with inputs dis no, source, temp, dest
        static public bool SolveTOH(int nDisc, Stack<int> source, Stack<int> temp, Stack<int> dest) 
        {
            if (nDisc <= 4)
            {
                //Apply TOH algorithm
                if ((nDisc % 2) == 0)
                {
                    Solve2DISCTOH(source, temp, dest);
                    nDisc = nDisc - 1;
                    if (nDisc == 1)
                        return true;

                    temp.Push(source.Pop());
                    movecount++;
                    PrintStack();
                    //New Source is Dest, New Temp is Source, New Dest is Temp
                    Solve2DISCTOH(dest, source, temp);
                    dest.Push(source.Pop());
                    movecount++;
                    PrintStack();
                    //New Source is Temp, New Temp is Source, New Dest is Dest
                    SolveTOH(nDisc, temp, source, dest);
                }
                else
                {
                    if (nDisc == 1)
                    {
                        return false;
                    }
                    Solve2DISCTOH(source, dest, temp);
                    nDisc = nDisc - 1;
                    dest.Push(source.Pop());
                    movecount++;
                    PrintStack();
                    Solve2DISCTOH(temp, source, dest);
                }
                return true;
            }
            else if (nDisc >= 5)
            {
                SolveTOH(nDisc - 2, source, temp, dest);
                temp.Push(source.Pop());
                movecount++;
                PrintStack();
                SolveTOH(nDisc - 2, dest, source, temp);
                dest.Push(source.Pop());
                movecount++;
                PrintStack();
                SolveTOH(nDisc - 1, temp, source, dest);
            }
            return true;
        
        }
        
        private void MainWindow_Load(object sender, EventArgs e)
        {
            discnumber_DD.Text = "6";
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            int maxdis = Convert.ToInt32(discnumber_DD.Text);
            DrawTower(towerA, maxdis, 120, 180, e.Graphics);
            DrawTower(towerB, maxdis, 320, 180, e.Graphics);
            DrawTower(towerC, maxdis, 520, 180, e.Graphics);

            graphics.Dispose();
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        public Stack<DiscInfo> towerA = new Stack<DiscInfo>();
        public Stack<DiscInfo> towerB = new Stack<DiscInfo>();
        private void discnumber_DD_SelectedIndexChanged(object sender, EventArgs e)
        {
            int maxdis = Convert.ToInt32(discnumber_DD.Text);
            movecount = 0;
            algorithms_steps.Clear();
            for (int i = maxdis; i >= 1; i--)
            {
                A.Push(i);
            }
            countA = A.Count;
            countB = B.Count;
            countC = C.Count;
            PrintStack();
            SolveTOH(maxdis, A, B, C);

            DiscInfo d1 = new DiscInfo(40, 25, Color.Green, 1);
            DiscInfo d2 = new DiscInfo(60, 30, Color.Blue, 2);
            DiscInfo d3 = new DiscInfo(80, 35, Color.YellowGreen, 3);
            DiscInfo d4 = new DiscInfo(100, 40, Color.Red, 4);
            DiscInfo d5 = new DiscInfo(120, 45, Color.Violet, 5);
            DiscInfo d6 = new DiscInfo(140, 50, Color.Purple, 6);
            DiscInfo d7 = new DiscInfo(160, 55, Color.OrangeRed, 7);
            DiscInfo d8 = new DiscInfo(180, 60, Color.Khaki, 8);
            DiscInfo d9 = new DiscInfo(200, 65, Color.IndianRed, 9);

            DiscInfo[] arrDisc = { d1, d2, d3, d4, d5, d6, d7, d8, d9 };

            towerA.Clear();
            towerB.Clear();
            towerC.Clear();
            for (int i = maxdis - 1; i >= 0 ; i--)
            {
                towerA.Push(arrDisc[i]);
            }
            currentStep = 0;
            moveTxt.Text = currentStep.ToString() + " Out Of " + algorithms_steps.Count.ToString();
            Invalidate();
        }
        private void moveBtn_Click(object sender, EventArgs e)
        {
            if (currentStep == algorithms_steps.Count)
            {
                return; // It Means No More Moves
            }

            MoveInfo info = algorithms_steps[currentStep++];

            moveTxt.Text = currentStep.ToString() + " Out Of " + algorithms_steps.Count.ToString();

            if (info.src == "A")
            {
                if (info.dest == "B")
                {
                    towerB.Push(towerA.Pop());
                }
                else 
                {
                    towerC.Push(towerA.Pop());
                }
            }
            else if(info.src == "B")
            {
                if(info.dest == "C") 
                {
                    towerC.Push(towerB.Pop());
                }
                else
                {
                    towerA.Push(towerB.Pop());
                }
            }
            else if (info.src == "C")
            {
                if (info.dest == "A")
                {
                    towerA.Push(towerC.Pop());
                }
                else
                {
                    towerB.Push(towerC.Pop());
                }
            }

            Invalidate();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public Stack<DiscInfo> towerC = new Stack<DiscInfo>();

        // Method For Drawing Tower
        // taking input Stack of Tower, Max No. Of Disc, X length, Y length
        // Graphics
        public void DrawTower(Stack<DiscInfo> tower, int maxDisc, int xbeg, int ybeg, Graphics graphics)
        {
            Stack<DiscInfo>.Enumerator et = tower.GetEnumerator();
            List<DiscInfo> dList = new List<DiscInfo>();
            int yoffset = maxDisc - tower.Count(); // 6 = max disc number


            while (true)
            {
                if (et.MoveNext() == false)
                    break;
              
                dList.Add(et.Current);
            }
            for (int i = dList.Count - 1; i >= 0; i--)
            {
                Rectangle r = new Rectangle(xbeg - dList[i].number * 10, ybeg + (i + yoffset) * 10, dList[i].width, dList[i].height);
                Pen p = new Pen(Color.Black);
                r.Y += 5;
                graphics.FillEllipse(dList[i].brush, r);
                graphics.DrawEllipse(p, r);
                r.Y -= 5;
                graphics.FillEllipse(dList[i].brush, r);
                graphics.DrawEllipse(p, r);
            }
        }
    }
}

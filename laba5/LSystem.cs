using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba5
{
    public partial class LSystem : Form
    {
        string atom;
        double angle;
        double startAngle;
        int maxIt;
        Dictionary<string, string> rules;
        List<Line> lines;
        public LSystem()
        {
            InitializeComponent();
            toolTip1=new ToolTip();
            toolTip1.SetToolTip(button1, "Отркыть файл, считать и нарисовать");
            toolTip2 = new ToolTip();
            toolTip2.SetToolTip(textBox1, "Максимальное количество итераций");
            rules = new Dictionary<string, string>();
          
            lines = new List<Line>();
            pictureBox1.Image = new Bitmap(pictureBox1.Width,pictureBox1.Height);
        }

        private void ParseRules(string[] lines)
        {
            maxIt = int.Parse(textBox1.Text);
            bool isFirst = true;
            foreach (string line in lines) {
                if (isFirst) { 
                    isFirst = false;
                    var t = line.Split();
                    atom = t[0];
                    startAngle = double.Parse(t[1]);
                    angle = double.Parse(t[2]);
                }
                else
                {
                    var l=line.Replace("->"," ");
                    var t = l.Split();
                    rules[t[0]] = t[1];

                }
            
            }
       //     Debug.WriteLine(rules.Count);
        }


        private string MakeIterations() { 
            StringBuilder sb = new StringBuilder();
            sb.Append(atom);
            for (int i = 0; i < maxIt; i++)
            {
                foreach (var k in rules.Keys)
                {
                    sb.Replace(k, rules[k]);
                }
            }

            return sb.ToString();
            
        }

        private double DegreeToRadian(double angle) {
            return angle * Math.PI / 180;
        }
        struct Line {
            public float x1, x2;
            public float y1, y2;
            public Line(float x1, float x2, float y1, float y2) { 
                this.x1 = x1; this.x2 = x2;
                this.y1 = y1; this.y2 = y2;
            }
        }
        private void DrawLSystem() {
            Random random = new Random();
            int level = 0;
            int maxLevel = 0;
            bool isRandom = false;

            float startX = pictureBox1.Width/2;
            float startY = pictureBox1.Height;

            float maxH = startY;
            float maxW = startX;
            float minH = startY;
            float minW = startX;

            string LSystem = MakeIterations();
            double currentAngle = startAngle;
            float newX;
            float newY;

            Stack<float> stack = new Stack<float>();
            List<int> levels = new List<int>();

            foreach (var c in LSystem) {
                if (c == 'F')
                {
                    if (isRandom)
                    {
                        newX = startX + 1 * (float)Math.Cos(DegreeToRadian(currentAngle + random.NextDouble() * angle));
                        newY = startY + 1 * (float)Math.Sin(DegreeToRadian(currentAngle + random.NextDouble() * angle));
                        isRandom = false;
                    }
                    else
                    {
                        newX = startX + 1 * (float)Math.Cos(DegreeToRadian(currentAngle));
                        newY = startY + 1 * (float)Math.Sin(DegreeToRadian(currentAngle));
                    }


                    if (newY > maxH)
                        maxH = newY;
                    if (newX > maxW)
                        maxW = newX;
                    if (newY < minH)
                        minH = newY;
                    if (newX < minW)
                        minW = newX;

                    lines.Add(new Line(startX, newX, startY, newY));
                    levels.Add(level);
                    if (level > maxLevel)
                        maxLevel = level;
                    startX = newX;
                    startY = newY;
                }

                else if (c == '[')
                {
                    level++;
                    stack.Push(startX);
                    stack.Push(startY);
                    stack.Push((float)currentAngle);
                  
                }

                else if (c == ']') {
                    level--;
                    currentAngle =(double)stack.Pop();
                    startY=stack.Pop();
                    startX=stack.Pop();
                    
                }

                else if (c == '@')
                    isRandom = true;

                else if (c == '+')
                    currentAngle += angle;
                else if (c == '-')
                    currentAngle -= angle;
               
            }

            Pen p = new Pen(Color.Black);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                int count = 0;
                float stepX = pictureBox1.Width / (maxW - minW);
                for (int i = 0; i < lines.Count; i++)
                {
                    float x1, x2;
                    float y1, y2;
                    x1 = (lines[i].x1 - minW) * stepX;
                    y1 = pictureBox1.Height * (lines[i].y1 - minH) / (maxH - minH);
                    x2 = (lines[i].x2 - minW) * stepX;
                    y2 = pictureBox1.Height * (lines[i].y2 - minH) / (maxH - minH);
                    if (!checkBox1.Checked)
                        g.DrawLine(p, x1, y1, x2, y2);
                    else {
                        int currl = levels[count];
                        double t = currl / (double)maxLevel;
                        int R = Math.Min(255, Math.Max(0, Color.Brown.R +(int)(t * (Color.Green.R - Color.Brown.R))));
                        int G = Math.Min(255, Math.Max(0, Color.Brown.G +(int)(t * (Color.Green.G - Color.Brown.G))));
                        int B = Math.Min(255, Math.Max(0, Color.Brown.B +(int)(t * (Color.Green.B - Color.Brown.B))));
                        Pen tr_p = new Pen(Color.FromArgb(R, G, B), maxLevel - currl+1);
                        g.DrawLine(tr_p, x1, y1, x2, y2);
                        count += 1;
                    }
                }

                pictureBox1.Invalidate();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string[] lines = File.ReadAllLines(filePath);
                ParseRules(lines);
                DrawLSystem();
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            lines.Clear();
            rules.Clear();
            pictureBox1.Invalidate();
        }
    }
}

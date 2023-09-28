using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba_graph_3
{
    public partial class Form4 : Form
    {
        private Point vertex1;
        private Point vertex2;
        private Point vertex3;
        private bool verticesSelected;
        Bitmap bmp;
        public Form4()
        {
            InitializeComponent();
            bmp = new Bitmap(pbCanvas.Width, pbCanvas.Height);
        }
       

        private void MainForm_Load(object sender, EventArgs e)
        {
            verticesSelected = false;
            pbCanvas.Paint += new PaintEventHandler(pbCanvas_Paint);
            pbCanvas.MouseClick += new MouseEventHandler(pbCanvas_MouseClick);
           
        }

        private void pbCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (!verticesSelected)
            {
                vertex1 = e.Location;
                verticesSelected = true;
            }
            else
            {
                if (vertex2.IsEmpty)
                {
                    vertex2 = e.Location;
                }
                else if (vertex3.IsEmpty)
                {
                    vertex3 = e.Location;
                    verticesSelected = false;
                    pbCanvas.Invalidate();
                }
            }
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (!vertex1.IsEmpty && !vertex2.IsEmpty && !vertex3.IsEmpty)
            {
                FillTriangleGradient(e.Graphics, vertex1, vertex2, vertex3, Color.Red, Color.Blue, Color.Green);
            }
        }

        private void FillTriangleGradient(Graphics gr, Point vertex1, Point vertex2, Point vertex3, Color color1, Color color2, Color color3)
        {
            int minX = Math.Min(Math.Min(vertex1.X, vertex2.X), vertex3.X);
            int minY = Math.Min(Math.Min(vertex1.Y, vertex2.Y), vertex3.Y);
            int maxX = Math.Max(Math.Max(vertex1.X, vertex2.X), vertex3.X);
            int maxY = Math.Max(Math.Max(vertex1.Y, vertex2.Y), vertex3.Y);
            
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (IsPointInsideTriangle(x, y, vertex1, vertex2, vertex3))
                    {
                        float w1 = CalculateBarycentricWeight(x, y, vertex2, vertex3, vertex1);
                        float w2 = CalculateBarycentricWeight(x, y, vertex1, vertex3, vertex2);
                        float w3 = CalculateBarycentricWeight(x, y, vertex3, vertex2, vertex1);

                        int r = (int)(w1 * color1.R + w2 * color2.R + w3 * color3.R);
                        int g = (int)(w1 * color1.G + w2 * color2.G + w3 * color3.G);
                        int b = (int)(w1 * color1.B + w2 * color2.B + w3 * color3.B);

                        Color pixelColor = Color.FromArgb(255, r, g, b);
                        //Debug.WriteLine(pixelColor.G);
                        //bmp.SetPixel(x, y, pixelColor); 
                        gr.FillRectangle(new SolidBrush(pixelColor), x, y, 1, 1);
                    }
                }
            }
            //pbCanvas.Image = bmp;
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            pbCanvas.Invalidate();
            //pbCanvas.Image = bmp;
        }
        private bool IsPointInsideTriangle(int x, int y, Point vertex1, Point vertex2, Point vertex3)
        {
            float d1 = CalculateOrientation(x, y, vertex1, vertex2);
            float d2 = CalculateOrientation(x, y, vertex2, vertex3);
            float d3 = CalculateOrientation(x, y, vertex3, vertex1);

            return (d1 > 0 && d2 > 0 && d3 > 0) || (d1 < 0 && d2 < 0 && d3 < 0);
        }

        private float CalculateOrientation(int x, int y, Point p1, Point p2)
        {
            return ((p2.X - p1.X) * (y - p1.Y) - (x - p1.X) * (p2.Y - p1.Y));
        }

        private float CalculateBarycentricWeight(int x, int y, Point p1, Point p2, Point p3)
        {
            float numerator = (p2.Y - p3.Y) * (x - p3.X) + (p3.X - p2.X) * (y - p3.Y);
            float denominator = (p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y);

            return numerator / denominator;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            verticesSelected = false;
            vertex1=Point.Empty; vertex2=Point.Empty; vertex3 = Point.Empty;
            pbCanvas.Invalidate();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba6
{
    public partial class Form1 : Form
    {
        Graphics g;
        Polyhedron figure;
        Pen figureDrawPen;
        Transformations transitions;
        bool isShowAxis;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            MyPoint.world = new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2);
            figureDrawPen = new Pen(Color.Black, 5);
            transitions = new Transformations();
            


        }
        #region Interface
        private void button1_Click(object sender, EventArgs e)
        {
            figure = new FigureCreator().getTr();
            ReDraw();
        }

        private void AxisCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isShowAxis = !isShowAxis;
            ReDraw();
        }

        private void mirrorButton_Click(object sender, EventArgs e)
        {
            if (XYRadioButton.Checked)
            {
                transitions.MirrorAroundAxis(figure, "XY");
            }
            if (XYRadioButton.Checked)
            {
                transitions.MirrorAroundAxis(figure, "XZ");
            }
            if (XYRadioButton.Checked)
            {
                transitions.MirrorAroundAxis(figure, "YZ");
            }
            ReDraw();
        }

        private void shiftButton_Click(object sender, EventArgs e)
        {
            float x = float.Parse(cXtextBox.Text, CultureInfo.InvariantCulture);
            float y = float.Parse(cYtextBox.Text, CultureInfo.InvariantCulture);
            float z = float.Parse(cZtextBox.Text, CultureInfo.InvariantCulture);

            transitions.Shift(figure, x, y, z);
            ReDraw();
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
            float x = float.Parse(sXtextBox.Text, CultureInfo.InvariantCulture);
            float y = float.Parse(sYtextBox.Text, CultureInfo.InvariantCulture);
            float z = float.Parse(sZtextBox.Text, CultureInfo.InvariantCulture);

            transitions.Scale(figure, x, y, z);
            ReDraw();
        }

        private void RotateAxisButton_Click(object sender, EventArgs e)
        {
            float degree = float.Parse(degreeTextBox.Text);

            if (XRadioButton.Checked)
            {
                transitions.RotateAroundCenterAxis(figure,degree, "X");
            }
            if (YRadioButton.Checked)
            {
                transitions.RotateAroundCenterAxis(figure,degree, "Y");
            }
            if (ZRadioButton.Checked)
            {
                transitions.RotateAroundCenterAxis(figure,degree, "Z");
            }
            ReDraw();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            figure = null;
            ReDraw();
        }


        #endregion
        #region Drawing
        // Drawing
        void DrawFigure(Polyhedron figure, Pen p)
        {
            foreach (Polygon poly in figure.Polygons)
            {
                DrawPoly(poly, p);
            }
        }

        void DrawPoly(Polygon p, Pen pen)
        {
            foreach (var line in p.Lines)
            {
                DrawLine(line, pen);
            }
        }

        void DrawLine(Line l, Pen p)
        {
            g.DrawLine(p, l.Start.Project(), l.End.Project());
        }

        void DrawAxis()
        {
            Line axisX = new Line(new MyPoint(0, 0, 0), new MyPoint(300, 0, 0));
            Line axisY = new Line(new MyPoint(0, 0, 0), new MyPoint(0, 300, 0));
            Line axisZ = new Line(new MyPoint(0, 0, 0), new MyPoint(0, 0, 300));

            DrawLine(axisX, new Pen(Color.Red, 5));
            DrawLine(axisY, new Pen(Color.Green, 5));
            DrawLine(axisZ, new Pen(Color.Blue, 5));


        }

        void ReDraw()
        {
            g.Clear(Color.White);
            if (figure!=null)
                DrawFigure(figure, figureDrawPen);
            if (isShowAxis)
                DrawAxis();
            //pictureBox1.Invalidate();
        }



        #endregion


    }

}

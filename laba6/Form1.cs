﻿using System;
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
        Transformations transformations;
        bool isShowAxis;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            Point.world = new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2);
            figureDrawPen = new Pen(Color.Black, 5);
            transformations = new Transformations();
            


        }
        #region Interface
        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "тетраэдр":
                    figure = new FigureCreator().GetTetrahedron();
                    break;
                case "гексаэдр":
                    figure = new FigureCreator().GetHexahedron();
                    break;
                case "октаэдр":
                    figure = new FigureCreator().GetOctahedron();
                    break;
                case "икосаэдр":
                    figure = new FigureCreator().GetIcosahedron();
                    break;
                case "додекаэдр":
                    figure = new FigureCreator().GetDodecahedron();
                    break;
                default:
                    throw new ArgumentException("invalid figure");
            }
            
            ReDraw();
        }

        private void AxisCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isShowAxis = !isShowAxis;
            ReDraw();
        }

        private void mirrorButton_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text=="XY")
            {
                transformations.MirrorAroundAxis(figure, "XY");
            }
            if (comboBox2.Text == "XZ")
            {
                transformations.MirrorAroundAxis(figure, "XZ");
            }
            if (comboBox2.Text == "YZ")
            {
                transformations.MirrorAroundAxis(figure, "YZ");
            }
            ReDraw();
        }

        private void shiftButton_Click(object sender, EventArgs e)
        {
            float x = float.Parse(cXtextBox.Text, CultureInfo.InvariantCulture);
            float y = float.Parse(cYtextBox.Text, CultureInfo.InvariantCulture);
            float z = float.Parse(cZtextBox.Text, CultureInfo.InvariantCulture);

            transformations.Shift(figure, x, y, z);
            ReDraw();
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
            float x = float.Parse(sXtextBox.Text, CultureInfo.InvariantCulture);
            float y = float.Parse(sYtextBox.Text, CultureInfo.InvariantCulture);
            float z = float.Parse(sZtextBox.Text, CultureInfo.InvariantCulture);

            transformations.Scale(figure, x, y, z);
            ReDraw();
        }

        private void RotateAxisButton_Click(object sender, EventArgs e)
        {
            float degree = float.Parse(degreeTextBox.Text);

            if (comboBox3.Text=="X")
            {
                transformations.RotateAroundCenterAxis(figure,degree, "X");
            }
            if (comboBox3.Text == "Y")
            {
                transformations.RotateAroundCenterAxis(figure,degree, "Y");
            }
            if (comboBox3.Text == "Z")
            {
                transformations.RotateAroundCenterAxis(figure,degree, "Z");
            }
            ReDraw();
        }


        private void RotateCustomAxisButton_Click(object sender, EventArgs e)
        {
            float x1=float.Parse(x1textBox.Text,CultureInfo.InvariantCulture);
            float y1=float.Parse(y1textBox.Text,CultureInfo.InvariantCulture);
            float z1=float.Parse(z1textBox.Text,CultureInfo.InvariantCulture);
            float x2=float.Parse(x2textBox.Text,CultureInfo.InvariantCulture);
            float y2=float.Parse(y2textBox.Text,CultureInfo.InvariantCulture);
            float z2=float.Parse(z2textBox.Text,CultureInfo.InvariantCulture);
            
            Point p1 = new Point(x1, y1, z1);
            Point p2 = new Point(x2, y2, z2);

            float degree = float.Parse(degreeCustom.Text);
            transformations.RotateAroundCustomAxis(figure,degree,p1,p2);
            ReDraw();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            figure = null;
            ReDraw();
        }
        private void perspectiveRadioButtom_CheckedChanged(object sender, EventArgs e)
        {
            Point.kind = ProjectionKind.PERSPECTIVE;
            ReDraw();
        }

        private void isometricRadioButtom_CheckedChanged(object sender, EventArgs e)
        {
            Point.kind = ProjectionKind.ISOMETRIC;
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
            g.DrawLine(p, l.Start.Projection(), l.End.Projection());
        }

        void DrawAxis()
        {
            Line axisX = new Line(new Point(0, 0, 0), new Point(300, 0, 0));
            Line axisY = new Line(new Point(0, 0, 0), new Point(0, 300, 0));
            Line axisZ = new Line(new Point(0, 0, 0), new Point(0, 0, 300));

            DrawLine(axisX, new Pen(Color.Red, 5));
            DrawLine(axisY, new Pen(Color.Green, 5));
            DrawLine(axisZ, new Pen(Color.Blue, 5));


        }

        void ReDraw()
        {
            g.Clear(Color.White);
            if (isShowAxis)
                DrawAxis();
            if (figure!=null)
                DrawFigure(figure, figureDrawPen);
            
            //pictureBox1.Invalidate();
        }




        #endregion


    }
}

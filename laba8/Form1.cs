using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba7
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen figureDrawPen;
        Pen figureHighPen;
        Transformations transformations;
        bool isShowAxis;
        Drawing drawing;
        int checkedIdx = 0;
        Camera camera;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            Point.world = new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2);
            Point.SetProjection(pictureBox1.Size, 1, 100, 45);
            figureDrawPen = new Pen(Color.Black, 2);
            figureHighPen = new Pen(Color.Red, 2);
            transformations = new Transformations();
            drawing = new Drawing(pictureBox1, g);
            camera = drawing.cam;
            drawing.figureDrawPen = figureDrawPen;
            drawing.highlightPen = figureHighPen;


        }
        #region Interface
        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "тетраэдр":
                    drawing.AddToScene(new FigureCreator().GetTetrahedron());
                    sceneFigures.Items.Add("TET");
                    break;
                case "гексаэдр":
                    //   drawing.scene[checkedIdx] = new FigureCreator().GetHexahedron();
                    break;
                case "октаэдр":
                    //   drawing.scene[checkedIdx] = new FigureCreator().GetOctahedron();
                    break;
                case "икосаэдр":
                    //   drawing.scene[checkedIdx] = new FigureCreator().GetIcosahedron();
                    break;
                case "додекаэдр":
                    //   drawing.scene[checkedIdx] = new FigureCreator().GetDodecahedron();
                    break;
                default:
                    throw new ArgumentException("invalid drawing.scene[checkedIdx]");
            }

            drawing.ReDraw(isShowAxis);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "obj files (*.obj)|*.obj";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                new DataManager().Save(saveFileDialog1.FileName, drawing.scene[checkedIdx]);
            }

        }
        private void loadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "obj files (*.obj)|*.obj";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                string filePath;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    drawing.AddToScene(new DataManager().Load(filePath));
                    sceneFigures.Items.Add("OBJ");
                }
            }
            drawing.ReDraw(isShowAxis);
        }

        private void AxisCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isShowAxis = !isShowAxis;
            drawing.ReDraw(isShowAxis);
        }

        private void mirrorButton_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "XY")
            {
                transformations.MirrorAroundAxis(drawing.scene[checkedIdx], "XY");
            }
            if (comboBox2.Text == "XZ")
            {
                transformations.MirrorAroundAxis(drawing.scene[checkedIdx], "XZ");
            }
            if (comboBox2.Text == "YZ")
            {
                transformations.MirrorAroundAxis(drawing.scene[checkedIdx], "YZ");
            }
            drawing.ReDraw(isShowAxis);
        }

        private void shiftButton_Click(object sender, EventArgs e)
        {
            float x = float.Parse(cXtextBox.Text);
            float y = float.Parse(cYtextBox.Text);
            float z = float.Parse(cZtextBox.Text);

            transformations.Shift(drawing.scene[checkedIdx], x, y, z);
            drawing.ReDraw(isShowAxis);
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
            float x = float.Parse(sXtextBox.Text);
            float y = float.Parse(sYtextBox.Text);
            float z = float.Parse(sZtextBox.Text);

            transformations.Scale(drawing.scene[checkedIdx], x, y, z);
            drawing.ReDraw(isShowAxis);
        }

        private void RotateAxisButton_Click(object sender, EventArgs e)
        {
            float degree = float.Parse(degreeTextBox.Text);

            if (comboBox3.Text == "X")
            {
                transformations.RotateAroundCenterAxis(drawing.scene[checkedIdx], degree, "X");
            }
            if (comboBox3.Text == "Y")
            {
                transformations.RotateAroundCenterAxis(drawing.scene[checkedIdx], degree, "Y");
            }
            if (comboBox3.Text == "Z")
            {
                transformations.RotateAroundCenterAxis(drawing.scene[checkedIdx], degree, "Z");
            }
            drawing.ReDraw(isShowAxis);
        }


        private void RotateCustomAxisButton_Click(object sender, EventArgs e)
        {
            float x1 = float.Parse(x1textBox.Text);
            float y1 = float.Parse(y1textBox.Text);
            float z1 = float.Parse(z1textBox.Text);
            float x2 = float.Parse(x2textBox.Text);
            float y2 = float.Parse(y2textBox.Text);
            float z2 = float.Parse(z2textBox.Text);

            Vertex p1 = new Vertex(x1, y1, z1);
            Vertex p2 = new Vertex(x2, y2, z2);

            float degree = float.Parse(degreeCustom.Text);
            transformations.RotateAroundCustomAxis(drawing.scene[checkedIdx], degree, p1, p2);
            drawing.ReDraw(isShowAxis);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            drawing.ClearScene();
            drawing.ReDraw(isShowAxis);
        }
        private void perspectiveRadioButtom_CheckedChanged(object sender, EventArgs e)
        {
            Point.kind = ProjectionKind.PERSPECTIVE;
            drawing.ReDraw(isShowAxis);
        }

        private void isometricRadioButtom_CheckedChanged(object sender, EventArgs e)
        {
            Point.kind = ProjectionKind.ISOMETRIC;
            drawing.ReDraw(isShowAxis);
        }

        private void figureRotButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                string filePath;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    drawing.AddToScene(new FigureCreator().CreateRotation(filePath));
                }
            }
            drawing.ReDraw(isShowAxis);
        }

        private void func1Button_Click(object sender, EventArgs e)
        {
            funcTextBox.Text = func1Button.Text;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            drawing.scene.Add(new FigureCreator().CreateFunction(x1functextBox.Text,
                                                        y1functextBox.Text,
                                                        x2functextBox.Text,
                                                        y2functextBox.Text,
                                                        hTextBox.Text,
                                                        hTextBox.Text,
                                                        funcTextBox.Text));
            drawing.ReDraw(isShowAxis);
        }

        #endregion





        private void x2textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void clearScene_Click(object sender, EventArgs e)
        {
            drawing.ClearScene();
        }

        private void sceneFigures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sceneFigures.SelectedIndex != -1)
            {
                drawing.scene[checkedIdx].isHighLighthed = false;
                checkedIdx = sceneFigures.SelectedIndex;
                drawing.scene[checkedIdx].isHighLighthed = true;
            }
        }

        private void drawingBox_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (drawingBox.SelectedIndex)
            {
                case 0:
                    drawing.kind = DRAWINGKIND.NORMAL;
                    break;
                case 1:
                    drawing.kind = DRAWINGKIND.NONFACIAL;
                    break;
                case 2:
                    drawing.kind = DRAWINGKIND.ZBUF;
                    break;
                default:
                    break;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                    drawing.cam.Move(forwardbackward: 5);
                    break;
                case 'a':
                    drawing.cam.Move(leftright: 5);
                    break;
                case 's':
                    drawing.cam.Move(forwardbackward: -5);
                    break;
                case 'd':
                    drawing.cam.Move(leftright: -5);
                    break;
                case 'q':
                    drawing.cam.Move(updown: 5);
                    break;
                case 'e':
                    drawing.cam.Move(updown: -5);
                    break;
                case 'i':
                    drawing.cam.ChangeView(shiftY: 2);
                    break;
                case 'j':
                    drawing.cam.ChangeView(shiftX: -2);
                    break;
                case 'k':
                    drawing.cam.ChangeView(shiftY: -2);
                    break;
                case 'l':
                    drawing.cam.ChangeView(shiftX: 2);
                    break;

                default: return;
            }
            drawing.ReDraw(isShowAxis);
        }
    }
}

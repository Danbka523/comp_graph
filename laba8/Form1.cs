using HonkSharp.Fluency;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba8
{
    public partial class Form1 : Form
    {
        Graphics g;
        Transformations transformations;
        bool isShowAxis;
        Drawing drawing;
        Camera camera;
        int figureCount = 1;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            Point.world = new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2);
            transformations = new Transformations();
            camera = new Camera();
            drawing = new Drawing(g, pictureBox1, camera);
            Point.transformations = transformations;
            Point.SetProjection(pictureBox1.Size, 1, 100, 45);

        }
        #region Interface
        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "тетраэдр":
                    drawing.AddToScene(new FigureCreator().GetTetrahedron());
                    listBox1.Items.Add($"TETRA {figureCount++}");
                    break;
                case "гексаэдр":
                    drawing.figure = new FigureCreator().GetHexahedron();
                    listBox1.Items.Add($"HEXA {figureCount++}");
                    break;
                case "октаэдр":
                    drawing.figure = new FigureCreator().GetOctahedron();
                    listBox1.Items.Add($"OCTA {figureCount++}");
                    break;
                case "икосаэдр":
                    drawing.figure = new FigureCreator().GetIcosahedron();
                    listBox1.Items.Add($"ICO {figureCount++}");
                    break;
                case "додекаэдр":
                    drawing.figure = new FigureCreator().GetDodecahedron();
                    listBox1.Items.Add($"DODE {figureCount++}");
                    break;
                default:
                    throw new ArgumentException("invalid drawing.figure");
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
                new DataManager().Save(saveFileDialog1.FileName, drawing.figure);
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
                    drawing.figure = new DataManager().Load(filePath);
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
                transformations.MirrorAroundAxis(drawing.figure, "XY");
            }
            if (comboBox2.Text == "XZ")
            {
                transformations.MirrorAroundAxis(drawing.figure, "XZ");
            }
            if (comboBox2.Text == "YZ")
            {
                transformations.MirrorAroundAxis(drawing.figure, "YZ");
            }
            drawing.ReDraw(isShowAxis);
        }

        private void shiftButton_Click(object sender, EventArgs e)
        {
            float x = float.Parse(cXtextBox.Text, CultureInfo.InvariantCulture);
            float y = float.Parse(cYtextBox.Text, CultureInfo.InvariantCulture);
            float z = float.Parse(cZtextBox.Text, CultureInfo.InvariantCulture);

            transformations.Shift(drawing.figure, x, y, z);
            drawing.ReDraw(isShowAxis);
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
            float x = float.Parse(sXtextBox.Text, CultureInfo.InvariantCulture);
            float y = float.Parse(sYtextBox.Text, CultureInfo.InvariantCulture);
            float z = float.Parse(sZtextBox.Text, CultureInfo.InvariantCulture);

            transformations.Scale(drawing.figure, x, y, z);
            drawing.ReDraw(isShowAxis);
        }

        private void RotateAxisButton_Click(object sender, EventArgs e)
        {
            float degree = float.Parse(degreeTextBox.Text);

            if (comboBox3.Text == "X")
            {
                transformations.RotateAroundCenterAxis(drawing.figure, degree, "X");
            }
            if (comboBox3.Text == "Y")
            {
                transformations.RotateAroundCenterAxis(drawing.figure, degree, "Y");
            }
            if (comboBox3.Text == "Z")
            {
                transformations.RotateAroundCenterAxis(drawing.figure, degree, "Z");
            }
            drawing.ReDraw(isShowAxis);
        }


        private void RotateCustomAxisButton_Click(object sender, EventArgs e)
        {
            float x1 = float.Parse(x1textBox.Text, CultureInfo.InvariantCulture);
            float y1 = float.Parse(y1textBox.Text, CultureInfo.InvariantCulture);
            float z1 = float.Parse(z1textBox.Text, CultureInfo.InvariantCulture);
            float x2 = float.Parse(x2textBox.Text, CultureInfo.InvariantCulture);
            float y2 = float.Parse(y2textBox.Text, CultureInfo.InvariantCulture);
            float z2 = float.Parse(z2textBox.Text, CultureInfo.InvariantCulture);

            Point p1 = new Point(x1, y1, z1);
            Point p2 = new Point(x2, y2, z2);

            float degree = float.Parse(degreeCustom.Text);
            transformations.RotateAroundCustomAxis(drawing.figure, degree, p1, p2);
            drawing.ReDraw(isShowAxis);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            drawing.figure = null;
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
                    drawing.figure = new FigureCreator().CreateRotation(filePath);
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
            drawing.figure = new FigureCreator().CreateFunction(x1functextBox.Text,
                                                        y1functextBox.Text,
                                                        x2functextBox.Text,
                                                        y2functextBox.Text,
                                                        hTextBox.Text,
                                                        hTextBox.Text,
                                                        funcTextBox.Text);
            drawing.ReDraw(isShowAxis);
        }

        #endregion

        private void x2textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = listBox1.SelectedIndex;
            if (idx != -1)
            {
                drawing.figure = drawing.sceneFigures[idx];
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w': camera.Move(forwardbackward: 5); break;
                case 'a': camera.Move(leftright: 5); break;
                case 's': camera.Move(forwardbackward: -5); break;
                case 'd': camera.Move(leftright: -5); break;
                case 'q': camera.Move(updown: 5); break;
                case 'e': camera.Move(updown: -5); break;
                case 'i': camera.ChangeView(shiftY: 2); break;
                case 'j': camera.ChangeView(shiftX: -2); break;
                case 'k': camera.ChangeView(shiftY: -2); break;
                case 'l': camera.ChangeView(shiftX: 2); break;
                default: return;
            }
            //if (isPruningFaces)
            //{
            //    shapeWithoutNonFacial = findNonFacial(sceneShapes[listBox.SelectedIndex], camera);
            //    redrawShapeWithoutNonFacial();
            //}
            //else
            drawing.ReDraw(isShowAxis);
            e.Handled = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba_graph_2_frame
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Bitmap bmp;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                textBox1.Text = filePath;
                bmp = new Bitmap(filePath);
                ShowGist(bmp);
            }

            pictureBox1.ImageLocation = textBox1.Text;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ShowGist(Bitmap bitmap) {

            Bitmap bitmapR = new Bitmap(bitmap.Width, bitmap.Height);
            Bitmap bitmapG = new Bitmap(bitmap.Width, bitmap.Height);
            Bitmap bitmapB = new Bitmap(bitmap.Width, bitmap.Height);

            List<int> Rcount = new List<int>(Enumerable.Repeat(0, 256));
            List<int> Gcount = new List<int>(Enumerable.Repeat(0, 256));
            List<int> Bcount = new List<int>(Enumerable.Repeat(0, 256));
            List<int> xValues = new List<int>(Enumerable.Range(0, 256));



            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    Rcount[pixelColor.R] += 1;
                    Gcount[pixelColor.G] += 1;
                    Bcount[pixelColor.B] += 1;

                    bitmapR.SetPixel(x, y, Color.FromArgb(pixelColor.A,pixelColor.R,0,0));
                    bitmapG.SetPixel(x, y, Color.FromArgb(pixelColor.A, 0,pixelColor.G, 0));
                    bitmapB.SetPixel(x, y, Color.FromArgb(pixelColor.A,0,0, pixelColor.B));
                }
            }


            //chart1.Series.Add("series1");
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 255;

            chart1.Series["Series1"].Color = Color.Red;
            chart1.Series["Series1"].Points.DataBindXY(xValues, Rcount);

            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = 255;
            chart2.Series["Series1"].Color = Color.Green;
            chart2.Series["Series1"].Points.DataBindXY(xValues, Gcount);

            chart3.ChartAreas[0].AxisX.Minimum = 0;
            chart3.ChartAreas[0].AxisX.Maximum = 255;
            chart3.Series["Series1"].Color = Color.Blue;
            chart3.Series["Series1"].Points.DataBindXY(xValues, Bcount);

            bitmapR.Save("bm_r.jpg");
            bitmapG.Save("bm_g.jpg");
            bitmapB.Save("bm_b.jpg");

            pictureBox2.Image = bitmapR;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.Image = bitmapG;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.Image = bitmapB;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void Form3_Load(object sender, EventArgs e)
        {
                
        }
    }
}

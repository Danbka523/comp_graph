using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace laba_graph_2_frame
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp_gray1;
            Bitmap bmp_gray2;
            Bitmap bmp;
            OpenFileDialog openFileDialog = new OpenFileDialog();
           
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                textBox1.Text = filePath;
                bmp = new Bitmap(filePath);
                bmp_gray1=DoGray1(bmp);
                bmp_gray2=DoGray2(bmp);
                GrayDiff(bmp_gray1, bmp_gray2);
            }

            pictureBox1.ImageLocation = textBox1.Text;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
           
           
        }

        private Bitmap DoGray1(Bitmap bmp) {
            Bitmap bitmap = new Bitmap(bmp);
         

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    int grayValue = (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G+0.114*pixelColor.B);
                    Color grayColor=Color.FromArgb(grayValue, grayValue, grayValue);   
                    bitmap.SetPixel(x, y, grayColor);
                }
            }
            bitmap.Save("grayscale1.jpg");
            pictureBox2.ImageLocation = "grayscale1.jpg";
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;


            List<int> count = new List<int>(Enumerable.Repeat(0,256));
            List<int> xValues = new List<int> ( Enumerable.Range(0,256));

       

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    count[pixelColor.R] += 1;
                }
            }


            //chart1.Series.Add("series1");
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 255;
            chart1.Series["Series1"].Points.DataBindXY(xValues, count);
            return bitmap;
        }


        private Bitmap DoGray2(Bitmap bmp)
        {
            Bitmap bitmap = new Bitmap(bmp);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    int grayValue = (int)(0.2126 * pixelColor.R + 0.7152 * pixelColor.G + 0.0722 * pixelColor.B);
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    bitmap.SetPixel(x, y, grayColor);
                }
            }
            bitmap.Save("grayscale2.jpg");
            pictureBox3.ImageLocation = "grayscale2.jpg";
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;



            List<int> count = new List<int>(Enumerable.Repeat(0, 256));
            List<int> xValues = new List<int>(Enumerable.Range(0, 256));



            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    count[pixelColor.R] += 1;
                }
            }


            //chart1.Series.Add("series1");
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = 255;
            chart2.Series["Series1"].Points.DataBindXY(xValues, count);


            return bitmap;
        }


        private void GrayDiff(Bitmap bmp1, Bitmap bmp2) {
            Bitmap bitmap = new Bitmap(bmp1.Width,bmp2.Height);
            for (int x = 0; x < bmp1.Width; x++)
            {
                for (int y = 0; y < bmp1.Height; y++)
                {
                    Color pixelColor1 = bmp1.GetPixel(x, y);
                    Color pixelColor2 = bmp2.GetPixel(x, y);
                    int grayValue = (Math.Abs(pixelColor2.R-pixelColor1.R) + Math.Abs(pixelColor2.G - pixelColor1.G) + Math.Abs(pixelColor2.B - pixelColor1.B));
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    bitmap.SetPixel(x, y, grayColor);
                }
            }
            bitmap.Save("grayscalediff.jpg");
            pictureBox4.ImageLocation = "grayscalediff.jpg";
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}

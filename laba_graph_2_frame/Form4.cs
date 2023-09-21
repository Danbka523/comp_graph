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

namespace laba_graph_2_frame
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            
        }
       
        Bitmap bmp;
        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                textBox1.Text = filePath;
                bmp = new Bitmap(filePath);
                
            }

            pictureBox1.ImageLocation = textBox1.Text;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.ImageLocation = textBox1.Text;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private (double h, double s, double v) PixelRGBtoHSV(Color pixelColor) {
            double r = pixelColor.R / 255.0;
            double g = pixelColor.G / 255.0;
            double b = pixelColor.B / 255.0;

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));

            double h, s, v;
            h = 0;
            if (max == min)
                h = 0;
            else if (max == r && g >= b)
                h=(60 * (g - b) / (max - min));
            else if (max == r && g < b)
                h=(60 * (g - b) / (max - min) + 360);
            else if (max == g)
                h = (60 * (b - r) / (max - min) + 120);
            else if (max == b)
                h = (60 * (r - g) / (max - min) + 240);

            s = max==0? 0: 1 - (min/max);

            v = max;
            return (h,s,v);

        }

        public Bitmap RGBtoHSV(Bitmap bitmap, double d_h = 0, double d_s = 0, double d_v = 0)
        {
          Bitmap hsv_bitmap = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color origPixelColor = bitmap.GetPixel(x, y);

                    (double h, double s, double v)=PixelRGBtoHSV(origPixelColor);

                    h += d_h;
                    s *= d_s;
                    v *= d_v;


                    Color hsv_pixel = HSVtoRGB(h, s, v);

                    hsv_bitmap.SetPixel(x, y, hsv_pixel);   

                }
            }
            return hsv_bitmap;
        }

        private Color HSVtoRGB(double h, double s, double v)
        {

          

            int hi = Convert.ToInt32(Math.Floor(h / 60)) % 6;
            double f = (h / 60) - Math.Floor(h / 60);
            double p = v * (1 - s);
            double q = v * (1 - f * s);
            double t = v * (1 - (1 - f) * s);

            int r, g, b;
            switch (hi)
            {
                case 0:
                    r = Convert.ToInt32(v * 255);
                    g = Convert.ToInt32(t * 255);
                    b = Convert.ToInt32(p * 255);
                    break;
                case 1:
                    r = Convert.ToInt32(q * 255);
                    g = Convert.ToInt32(v * 255);
                    b = Convert.ToInt32(p * 255);
                    break;
                case 2:
                    r = Convert.ToInt32(p * 255);
                    g = Convert.ToInt32(v * 255);
                    b = Convert.ToInt32(t * 255);
                    break;
                case 3:
                    r = Convert.ToInt32(p * 255);
                    g = Convert.ToInt32(q * 255);
                    b = Convert.ToInt32(v * 255);
                    break;
                case 4:
                    r = Convert.ToInt32(t * 255);
                    g = Convert.ToInt32(p * 255);
                    b = Convert.ToInt32(v * 255);
                    break;
                default:
                    r = Convert.ToInt32(v * 255);
                    g = Convert.ToInt32(p * 255);
                    b = Convert.ToInt32(q * 255);
                    break;
            }

            return Color.FromArgb(r, g, b);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pictureBox2.Image=RGBtoHSV(bmp,trackBar1.Value*36, trackBar2.Value/10.0,trackBar3.Value / 10.0);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            pictureBox2.Image = RGBtoHSV(bmp, trackBar1.Value * 36, trackBar2.Value / 10.0, trackBar3.Value / 10.0);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            pictureBox2.Image = RGBtoHSV(bmp, trackBar1.Value * 36, trackBar2.Value / 10.0, trackBar3.Value / 10.0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RGBtoHSV(bmp, trackBar1.Value * 36, trackBar2.Value / 10.0, trackBar3.Value / 10.0).Save("hsv.jpg");
        }
    }
}

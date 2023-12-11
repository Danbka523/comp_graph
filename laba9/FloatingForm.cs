using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba7
{
    public partial class FloatingForm : Form
    {
        FloatingDrawing fLoatingDrawing;
        Drawing drawing;
        Graphics g;
        public FloatingForm(string func)
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            drawing = new Drawing(pictureBox1, g);
            fLoatingDrawing = new FloatingDrawing(pictureBox1, func, drawing);
            Point.screenSize = pictureBox1.Size;
            fLoatingDrawing.ReDraw();
        }

        private void trianglesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (trianglesRadioButton.Checked)
            {
                fLoatingDrawing.displayType = DISPLAYTYPE.TRIANGLES;
                fLoatingDrawing.ReDraw();
            }
        }

        private void linesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (linesRadioButton.Checked)
            {
                fLoatingDrawing.displayType = DISPLAYTYPE.LINES;
                fLoatingDrawing.ReDraw();
            }
        }

        private void netRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (netRadioButton.Checked)
            {
                fLoatingDrawing.displayType = DISPLAYTYPE.NET;
                fLoatingDrawing.ReDraw();
            }
        }

        private void FloatingForm_KeyPress(object sender, KeyPressEventArgs e)
        {

            switch (e.KeyChar)
            {
                case 'w':
                    fLoatingDrawing.changeViewAngles(shiftY: 2);
                    break;
                case 'a':
                    fLoatingDrawing.changeViewAngles(shiftX: -2);
                    break;
                case 's':
                    fLoatingDrawing.changeViewAngles(shiftY: -2);
                    break;
                case 'd':
                    fLoatingDrawing.changeViewAngles(shiftX: 2);
                    break;
                default: return;
            }
            fLoatingDrawing.ReDraw();
            e.Handled = true;
        }
    }
}

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
    public partial class CornishRoom : Form
    {
        bool isMirror;
        bool isTrans;
        bool isLight;
        public CornishRoom()
        {
            InitializeComponent();
            wallComboBox.SelectedIndex = 1;
        }

        private void createRoomButton_Click(object sender, EventArgs e)
        {
            Scene scene = new Scene(pictureBox1.Width, pictureBox1.Height, isMirror, isTrans, isLight);
            scene.Load();
            pictureBox1.Image = scene.Draw();
        }

        private void reflectCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (reflectCheck.Checked)
            {
                isMirror = true;
            }
            else
                isMirror = false;
        }

        private void transparencyCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (transparencyCheck.Checked)
            {
                isTrans = true;
            }
            else
                isTrans = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                isLight = true;
            }
            else
                isLight = false;
        }
    }
}

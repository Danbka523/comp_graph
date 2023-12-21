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
        public CornishRoom()
        {
            InitializeComponent();
            wallComboBox.SelectedIndex = 1;
        }

        private void createRoomButton_Click(object sender, EventArgs e)
        {
            Scene scene = new Scene(pictureBox1.Width, pictureBox1.Height);
            scene.Load();
            pictureBox1.Image = scene.Draw();
        }
    }
}
